﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UGraph.Propertys;
using UGraph.Edge;
using UGraph.Exceptions;

namespace UGraph.IncidenceMatrix
{
    public class IncidenceMatrix<TVertex, TGraphType> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        private readonly Type graphType;
        private List<List<double?>> matrix;
        private Stack<int> empty_vertex_indexes;
        private Stack<int> empty_edge_indexes;
        private Dictionary<TVertex, int> vertex_index_map;
        private Dictionary<int, TVertex> index_vertex_map;
        private Dictionary<Edge<TVertex>, int> index_edge_map;
        public int Count { get; private set; }

        private IEnumerator<TVertex> current_vertex;

        public IncidenceMatrix()
        {
            graphType = typeof(TGraphType);
            matrix = new List<List<double?>>();
            empty_vertex_indexes = new Stack<int>();
            empty_edge_indexes = new Stack<int>();
            vertex_index_map = new Dictionary<TVertex, int>();
            index_vertex_map = new Dictionary<int, TVertex>();
            index_edge_map = new Dictionary<Edge<TVertex>, int>();
            current_vertex = null;
        }

        public override bool MoveIterator()
        {
            if (current_vertex is null)
            {
                ResetIterator();
            }

            if (!current_vertex.MoveNext())
            {
                current_vertex = null;
                return false;
            }

            return true;
        }

        public override TVertex GetIteratorValue()
        {
            return current_vertex.Current;
        }

        public override void ResetIterator()
        {
            current_vertex = vertex_index_map.Keys.GetEnumerator();
        }

        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException("A vertex can not be null");
            if (empty_vertex_indexes.Count == 0)
            {
                vertex_index_map.Add(vertex, matrix.Count);
                index_vertex_map.Add(matrix.Count, vertex);
                matrix.Add(new List<double?>());
            }
            else
            {
                int index = empty_vertex_indexes.Pop();
                vertex_index_map.Add(vertex, index);
                index_vertex_map.Add(index, vertex);
            }

            Count++;
        }

        public override void RemoveVertex(TVertex vertex)
        {
            try
            {
                int index = vertex_index_map[vertex];
                empty_vertex_indexes.Push(index);
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != EMPTY_EDGE)
                    {
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            if (matrix[j][i] != EMPTY_EDGE)
                            {
                                matrix[j][i] = EMPTY_EDGE;
                                break;
                            }
                        }
                    }

                    matrix[index][i] = EMPTY_EDGE;
                }

                vertex_index_map.Remove(vertex);
                index_vertex_map.Remove(index);
                Count--;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            }
        }

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            if (empty_edge_indexes.Count == 0)
            {
                foreach (List<double?> list in matrix)
                {
                    list.Add(0);
                }

                matrix[vertex_index_map[edge.GetSource()]][matrix[0].Count - 1] = edge.GetWeight();
                matrix[vertex_index_map[edge.GetDestination()]][matrix[0].Count - 1] = null;
                index_edge_map.Add(edge, matrix[0].Count - 1);
                if (!edge.Source.Equals(edge.Destination))
                {
                    index_edge_map.Add(new Edge<TVertex>(edge.Destination, edge.Source, edge.Weight),
                        matrix[0].Count - 1);
                }
            }
            else
            {
                int index = empty_edge_indexes.Pop();
                matrix[vertex_index_map[edge.GetSource()]][index] = edge.GetWeight();
                matrix[vertex_index_map[edge.GetDestination()]][index] = edge.GetWeight();
                index_edge_map.Add(edge, index);
                index_edge_map.Add(new Edge<TVertex>(edge.Destination, edge.Source, edge.Weight), index);
            }
        }

        private void AddEdgeDirectedGraph(Edge<TVertex> edge)
        {
            if (empty_edge_indexes.Count == 0)
            {
                foreach (List<double?> list in matrix)
                {
                    list.Add(0);
                }

                matrix[vertex_index_map[edge.GetSource()]][matrix[0].Count - 1] = edge.GetWeight();
                matrix[vertex_index_map[edge.GetDestination()]][matrix[0].Count - 1] = null;
                index_edge_map.Add(edge, matrix[0].Count - 1);
            }
            else
            {
                int index = empty_edge_indexes.Pop();
                matrix[vertex_index_map[edge.GetSource()]][index] = edge.GetWeight();
                matrix[vertex_index_map[edge.GetDestination()]][index] = null;
                index_edge_map.Add(edge, index);
            }
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            try
            {
                if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
                else AddEdgeUndirectedGraph(edge);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidEdgeException($"Edge {edge} has invalid vertexes");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidEdgeException($"Edge {edge} is not valid");
            }
        }

        private bool UpdateEdgeWeightDirectedGraph(Edge<TVertex> edge, double weight)
        {
            bool result = index_edge_map.ContainsKey(edge);
            if (result)
            {
                int index = index_edge_map[edge];
                for (int i = 0; i < matrix.Count; i++)
                {
                    if (matrix[i][index] != 0 && matrix[i][index] != null)
                    {
                        matrix[i][index] = weight;
                        break;
                    }
                }
            }

            return result;
        }

        private bool UpdateEdgeWeightUndirectedGraph(Edge<TVertex> edge, double weight)
        {
            bool result = index_edge_map.ContainsKey(edge) && index_edge_map.ContainsKey(edge);
            if (result)
            {
                int index = index_edge_map[edge];
                int count = 0;
                for (int i = 0; i < matrix.Count; i++)
                {
                    if (matrix[i][index] != 0 && matrix[i][index] != null)
                    {
                        matrix[i][index] = weight;
                        count++;
                    }

                    if (count == 2) break;
                }
            }

            return result;
        }

        public override void UpdateEdgeWeight(Edge<TVertex> edge, double weight)
        {
            bool result;
            if (graphType == typeof(Directed))
            {
                result = UpdateEdgeWeightDirectedGraph(edge, weight);
            }
            else
            {
                result = UpdateEdgeWeightUndirectedGraph(edge, weight);
            }

            if (!result)
            {
                // Levantar excecao
            }
        }

        public override void RemoveEdge(Edge<TVertex> edge)
        {
            try
            {
                int index = index_edge_map[edge];
                int counter = 0;
                for (int i = 0; i < matrix.Count; i++)
                {
                    if (matrix[i][index] != 0)
                    {
                        matrix[i][index] = 0;
                        counter++;
                    }

                    if (counter == 2) break;
                }

                empty_edge_indexes.Push(index);
                index_edge_map.Remove(edge);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidEdgeException($"Edge {edge} has invalid vertexes");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidEdgeException($"Edge {edge} does not exist in the graph");
            }
        }

        public override void ClearEdges()
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    matrix[i][j] = 0;
                }
            }

            for (int i = 0; i < matrix[0].Count; i++)
            {
                empty_edge_indexes.Push(i);
            }

            index_edge_map.Clear();
        }

        public override bool Contains(TVertex vertex)
        {
            return vertex_index_map.ContainsKey(vertex);
        }

        public override bool AreConnected(TVertex a, TVertex b)
        {
            return index_edge_map.ContainsKey(new Edge<TVertex>(a, b));
        }

        public override int GetCount()
        {
            return Count;
        }

        protected override IEnumerator<OutEdge<TVertex>> GetAdjacentVertexes(TVertex vertex)
        {
            List<OutEdge<TVertex>> adjacents = new List<OutEdge<TVertex>>();
            try
            {
                int index = vertex_index_map[vertex];
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != null && matrix[index][i] != 0)
                    {
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            if ((graphType == typeof(Directed) && matrix[j][i] == null) ||
                                (graphType == typeof(Undirected) && matrix[j][i] != 0))
                            {
                                adjacents.Add(new OutEdge<TVertex>(index_vertex_map[j], (double)matrix[index][i]));
                                break;
                            }
                        }
                    }
                }

                return adjacents.GetEnumerator();
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            }
        }

        public override List<TVertex> GetAdjacencyList(TVertex vertex)
        {
            List<TVertex> adjacents = new List<TVertex>();
            try
            {
                int index = vertex_index_map[vertex];
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != null && matrix[index][i] != 0)
                    {
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            if ((graphType == typeof(Directed) && matrix[j][i] == null) ||
                                (graphType == typeof(Undirected) && matrix[j][i] != 0))
                            {
                                adjacents.Add(index_vertex_map[j]);
                                break;
                            }
                        }
                    }
                }

                return adjacents;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            }
        }
    }
}