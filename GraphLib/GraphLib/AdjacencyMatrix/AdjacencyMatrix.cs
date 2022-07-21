using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphLib.Propertys;
using GraphLib.Edge;
using GraphLib.Exceptions;

namespace GraphLib.AdjacencyMatrix
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
        public int Count { get; private set; }

        public AdjacencyMatrix()
        {
            graphType = typeof(TGraphType);
            matrix = new List<List<double>>();
            empty_indexes = new Stack<int>();
            vertex_index_map = new Dictionary<TVertex, int>();
            index_vertex_map = new Dictionary<int, TVertex>();
        } 
        
        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException("A vertex can not be null");
            if (empty_indexes.Count == 0)
            {
                matrix.Add(new List<double>(Count + 1));
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
                for (int i = 0; i < matrix.Count; i++)
                {
                    matrix[i][index] = EMPTY_EDGE;
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
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWeight();
            matrix[vertex_index_map[edge.GetDestination()]][vertex_index_map[edge.GetSource()]] = edge.GetWeight();
        }
        
        private void AddEdgeDirectedGraph(Edge<TVertex> edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWeight();
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
            try
            {
                if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
                else RemoveEdgeUndirectedGraph(edge);
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

        public override bool Contains(TVertex vertex)
        {
            return vertex_index_map.ContainsKey(vertex);
        }

        public override bool AreConected(TVertex a, TVertex b)
        {
            try
            {
                return matrix[vertex_index_map[a]][vertex_index_map[b]] != 0;
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidEdgeException("A vertex can not be null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidEdgeException("Invalid vertexes");
            }
        }

        public override int GetCount()
        {
            return Count;
        }

        public override IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex)
        {
            List<OutEdge<TVertex>> neighbours = new List<OutEdge<TVertex>>();
            try
            {
                int index = vertex_index_map[vertex];
                for (int i = 0; i < matrix[index].Count; i++)
                {
                    if (matrix[index][i] != 0)
                    {
                        neighbours.Add(new OutEdge<TVertex>(index_vertex_map[index], matrix[index][i]));
                    }
                }
                return neighbours.GetEnumerator();
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
