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
        where TGraphType : GraphType
        where TEdge : IEdge<TVertex>
    {
        private Type graphType;
        private List<List<double>> matrix;
        private Stack<int> empty_indexes;
        private Dictionary<TVertex, int> index_map;
        public int Cout { get; private set; }

        public AdjacencyMatrix()
        {
            graphType = typeof(TGraphType);
            matrix = new List<List<double>>();
            empty_indexes = new Stack<int>();
            index_map = new Dictionary<TVertex, int>();
        } 
        
        public override void AddVertex(TVertex vertex)
        {
            if (empty_indexes.Count == 0)
            {
                matrix.Add(new List<double>());
                index_map.Add(vertex, matrix.Count - 1);
            }
            else
            {
                int index = empty_indexes.Pop();
                index_map.Add(vertex, index);
            }
            Cout++;
        }

        public override void RemoveVertex(TVertex vertex)
        {
            int index = index_map[vertex];
            empty_indexes.Push(index);
            for (int i = 0; i < matrix[index].Count; i++)
            {
                matrix[index][i] = EMPTY_EDGE;
            }
            index_map.Remove(vertex);
            Cout--;
        }
        
        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            matrix[index_map[edge.GetSource()]][index_map[edge.GetDestination()]] = edge.GetWheight();
            matrix[index_map[edge.GetDestination()]][index_map[edge.GetSource()]] = edge.GetWheight();
        }
        
        private void AddEdgeEdgeDirectedGraph(TEdge edge)
        {
            matrix[index_map[edge.GetSource()]][index_map[edge.GetDestination()]] = edge.GetWheight();
        }
        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) AddEdgeEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);     
        }
        
         private void RemoveEdgeUndirectedGraph(TEdge edge)
         {
            matrix[index_map[edge.GetSource()]][index_map[edge.GetDestination()]] = 0;
            matrix[index_map[edge.GetDestination()]][index_map[edge.GetSource()]] = 0;
         }
 
         private void RemoveEdgeDirectedGraph(TEdge edge)
         {
            matrix[index_map[edge.GetSource()]][index_map[edge.GetDestination()]] = 0;
         }       

        public override void RemoveEdge(TEdge edge)
        {            
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
            else RemoveEdgeUndirectedGraph(edge);
        }
    }
}
