using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphLib.Propertys;
using GraphLib.Edge;

namespace GraphLib.IncidenceMatrix
{
    public class IncidenceMatrix<TVertex, TEdge, TGraphType> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TEdge : IEdge<TVertex>
        where TGraphType : GraphType
    {
        private readonly Type graphType;
        private List<List<double?>> matrix;
        private Stack<int> empty_vertex_indexes;
        private Stack<int> empty_edge_indexes;
        private Dictionary<TVertex, int> vertex_index_map;
        private Dictionary<int, TVertex> index_vertex_map;
        private Dictionary<TEdge, int> index_edge_map;
        public int Count { get; private set; }

        public IncidenceMatrix()
        {
            graphType = typeof(TGraphType);
            matrix = new List<List<double?>>();
            empty_vertex_indexes = new Stack<int>();
            empty_edge_indexes = new Stack<int>();
            vertex_index_map = new Dictionary<TVertex, int>();
            index_vertex_map = new Dictionary<int, TVertex>();
            index_edge_map = new Dictionary<TEdge, int>();
        } 
        
        public override void AddVertex(TVertex vertex)
        {
            if (empty_vertex_indexes.Count == 0)
            {
                vertex_index_map.Add(vertex, matrix.Count - 1);
                index_vertex_map.Add(matrix.Count - 1, vertex);
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
        
        private void AddEdgeUndirectedGraph(TEdge edge)
        {
             if (empty_edge_indexes.Count == 0)
             {
                 foreach (List<double?> list in matrix)
                 {
                    list.Add(0); 
                 }
                 matrix[vertex_index_map[edge.GetSource()]][matrix[0].Count] = edge.GetWheight();
                 matrix[vertex_index_map[edge.GetDestination()]][matrix[0].Count] = null;
             }
             else
             {
                 int index = empty_edge_indexes.Pop();
                 matrix[vertex_index_map[edge.GetSource()]][index] = edge.GetWheight();
                 matrix[vertex_index_map[edge.GetDestination()]][index] = edge.GetWheight();
             }                   
        }
        
        private void AddEdgeDirectedGraph(TEdge edge)
        {
            if (empty_edge_indexes.Count == 0)
            {
                foreach (List<double?> list in matrix)
                {
                   list.Add(0); 
                }
                matrix[vertex_index_map[edge.GetSource()]][matrix[0].Count] = edge.GetWheight();
                matrix[vertex_index_map[edge.GetDestination()]][matrix[0].Count] = null;
            }
            else
            {
                int index = empty_edge_indexes.Pop();
                matrix[vertex_index_map[edge.GetSource()]][index] = edge.GetWheight();
                matrix[vertex_index_map[edge.GetDestination()]][index] = null;
            }
        }
        
        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);     
        }
        
        public override void RemoveEdge(TEdge edge)
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
                if (matrix[index][i] != null && matrix[index][i] != 0)
                {
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        // Null eh igual a 0 ???
                        if (matrix[j][i] == null || matrix[j][i] != 0)
                        {
                            neighbours.Add(new OutEdge<TVertex>(index_vertex_map[j], (double) matrix[index][i]));
                            break;
                        }
                    }
                }
            }
            return neighbours.GetEnumerator();
        }
    }
}
