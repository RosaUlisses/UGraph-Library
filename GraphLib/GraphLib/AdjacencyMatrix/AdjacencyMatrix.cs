using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphLib.Propertys;
using GraphLib.Edge;

namespace GraphLib.AdjacencyMatrix
{
    public class AdjacencyMatrix<TVertex, TEdge, TGraphType> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TEdge : IEdge<TVertex>
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
        
        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWheight();
            matrix[vertex_index_map[edge.GetDestination()]][vertex_index_map[edge.GetSource()]] = edge.GetWheight();
        }
        
        private void AddEdgeDirectedGraph(TEdge edge)
        {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = edge.GetWheight();
        }
        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);     
        }
        
         private void RemoveEdgeUndirectedGraph(TEdge edge)
         {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = 0;
            matrix[vertex_index_map[edge.GetDestination()]][vertex_index_map[edge.GetSource()]] = 0;
         }
 
         private void RemoveEdgeDirectedGraph(TEdge edge)
         {
            matrix[vertex_index_map[edge.GetSource()]][vertex_index_map[edge.GetDestination()]] = 0;
         }       

        public override void RemoveEdge(TEdge edge)
        {            
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
            else RemoveEdgeUndirectedGraph(edge);
        }
        public override int GetCount()
        {
            return Count;
        }

        public override IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex)
        {
            // TODO -> levantar execao se o vertice nao existir
            List<OutEdge<TVertex>> neighbours = new List<OutEdge<TVertex>>();
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
    }
}
