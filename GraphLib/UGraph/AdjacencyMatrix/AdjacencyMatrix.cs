﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UGraph.Propertys;
using UGraph.Exceptions;

namespace UGraph.AdjacencyMatrix
{
    public class AdjacencyMatrix<TVertex, TGraphType> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        private readonly Type graphType;
        private List<List<double>> matrix;
        private Stack<int> empty_indexes;
        private Dictionary<TVertex, int> vertex_index_map;
        private Dictionary<int, TVertex> index_vertex_map;

        private IEnumerator<TVertex> current_vertex;
        public int Count { get; private set; }

        public AdjacencyMatrix()
        {
            graphType = typeof(TGraphType);
            matrix = new List<List<double>>();
            empty_indexes = new Stack<int>();
            vertex_index_map = new Dictionary<TVertex, int>();
            index_vertex_map = new Dictionary<int, TVertex>();
            current_vertex = null;
        }

        public AdjacencyMatrix(Graph<TVertex, TGraphType> graph)
        {
            if (graph is null)
            {
                throw new InvalidGraphException($"Graph {nameof(graph)} is null");
            }

            graphType = typeof(TGraphType);
            matrix = new List<List<double>>();
            empty_indexes = new Stack<int>();
            vertex_index_map = new Dictionary<TVertex, int>();
            index_vertex_map = new Dictionary<int, TVertex>();
            current_vertex = null;

            foreach (TVertex vertex in graph)
            {
                AddVertex(vertex);
            }

            List<Edge<TVertex>> edges = graph.GetEdgeList();
            foreach (Edge<TVertex> edge in edges)
            {
                AddEdge(new Edge<TVertex>(edge.Source, edge.Destination, edge.Weight));
            }
        }

        protected override bool MoveIterator()
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

        protected override TVertex GetIteratorValue()
        {
            return current_vertex.Current;
        }

        protected override void ResetIterator()
        {
            current_vertex = vertex_index_map.Keys.GetEnumerator();
        }

        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            try
            {
                if (empty_indexes.Count == 0)
                {
                    matrix.Add(new List<double>(new double[Count + 1]));
                    foreach (List<double> list in matrix)
                    {
                        list.Add(0);
                    }

                    vertex_index_map.Add(vertex, matrix.Count - 1);
                    index_vertex_map.Add(matrix.Count - 1, vertex);
                }
                else
                {
                    int index = empty_indexes.Pop();
                    vertex_index_map.Add(vertex, index);
                    index_vertex_map.Add(index, vertex);
                }

                Count++;
            }
            catch (ArgumentException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} already exists in the graph");
            }
        }

        public override void RemoveVertex(TVertex vertex)
        {
            try
            {
                int index = vertex_index_map[vertex];
                empty_indexes.Push(index);
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    matrix[index][i] = EMPTY_EDGE;
                }

                foreach (List<double> list in matrix)
                {
                    list[index] = EMPTY_EDGE;
                }

                vertex_index_map.Remove(vertex);
                index_vertex_map.Remove(index);
                Count--;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }
        }

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWeight();
            matrix[vertex_index_map[edge.GetDestination()]][vertex_index_map[edge.GetSource()]] = edge.GetWeight();
        }

        private void AddEdgeDirectedGraph(Edge<TVertex> edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWeight();
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            try
            {
                if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
                else AddEdgeUndirectedGraph(edge);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidEdgeException($"One or both vertexes of edge {nameof(edge)} are null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidEdgeException(
                    $"One or both vertexes of edge {nameof(edge)} do not exist in the graph");
            }
        }

        private void RemoveEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = 0;
            matrix[vertex_index_map[edge.GetDestination()]][vertex_index_map[edge.GetSource()]] = 0;
        }

        private void RemoveEdgeDirectedGraph(Edge<TVertex> edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = 0;
        }

        public override void RemoveEdge(Edge<TVertex> edge)
        {
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            try
            {
                if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
                else RemoveEdgeUndirectedGraph(edge);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidEdgeException($"One or both vertexes of edge {nameof(edge)} are null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidEdgeException(
                    $"One or both vertexes of edge {nameof(edge)} do not exist in the graph");
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
        }

        public override bool Contains(TVertex vertex)
        {
            return vertex_index_map.ContainsKey(vertex);
        }

        public override bool AreConnected(TVertex a, TVertex b)
        {
            try
            {
                return matrix[vertex_index_map[a]][vertex_index_map[b]] != 0;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"One or both of vertexes {a} and {b} are null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"One or both of vertexes {a} and {b} do not exist in the graph");
            }
        }

        public override int GetCount()
        {
            return Count;
        }

        protected override IEnumerator<OutEdge<TVertex>> GetEdgesFromVertex(TVertex vertex)
        {
            List<OutEdge<TVertex>> adjacents = new List<OutEdge<TVertex>>();
            try
            {
                int index = vertex_index_map[vertex];
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != 0)
                    {
                        adjacents.Add(new OutEdge<TVertex>(index_vertex_map[i], matrix[index][i]));
                    }
                }

                return adjacents.GetEnumerator();
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }
        }

        protected override Graph<TVertex, TGraphType> GetTransposedGraph()
        {
            Graph<TVertex, TGraphType> transposedGraph = new AdjacencyMatrix<TVertex, TGraphType>();
            foreach (TVertex vertex in this)
            {
                transposedGraph.AddVertex(vertex);
            }

            IEnumerator<Edge<TVertex>> edges = GetAllEdges();
            while (edges.MoveNext())
            {
                transposedGraph.AddEdge(new Edge<TVertex>(edges.Current.Destination, edges.Current.Source,
                    edges.Current.Weight));
            }

            return transposedGraph;
        }

        public override List<TVertex> GetAdjacencyList(TVertex vertex)
        {
            List<TVertex> adjacents = new List<TVertex>();
            try
            {
                int index = vertex_index_map[vertex];
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != 0)
                    {
                        adjacents.Add(index_vertex_map[i]);
                    }
                }

                return adjacents;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }
        }
    }
}