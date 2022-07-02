using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphLib.Edge;

namespace GraphLib
{
    public class AdjacencyList<TVertex, TEdge, TWheight, TGraphType> :
        Graph<TVertex, TEdge, TWheight, TGraphType> where TEdge : IEdge<TVertex, TGraphType>
    {
        private Dictionary<TVertex, List<OutEdge<TVertex>>> adjacency_lists;
        public int Count { get; }

        public AdjacencyList()
        {
            adjacency_lists = new Dictionary<TVertex, List<OutEdge<TVertex>>>();
            Count = 0;
        }

        public override void AddVertex(TVertex vertex)
        {
            // TODO -> levantar execao caso vertex seja null 
            adjacency_lists.Add(vertex, new List<OutEdge<TVertex>>());
        }

        public override void RemoveVertex(TVertex vertex)
        {
            // TODO -> levantar execao caso vertex seja null 
            adjacency_lists.Remove(vertex);
        }

        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (edge.HasWeight())
            {
                adjacency_lists[edge.GetSource()]
                    .Add(new WheightedOutEdge<TVertex, TWheight>((edge as IEdge<TVertex, TWheight>)));
            }
            else
            {
                adjacency_lists[edge.GetSource()].Add(new((edge.GetDestination())));
            }
        }

        public override void RemoveEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (edge.HasWeight())
            {
                adjacency_lists[edge.GetSource()]
                    .Remove(new WheightedOutEdge<TVertex, TWheight>((edge as IEdge<TVertex, TWheight>)));
            }
            else
            {
                adjacency_lists[edge.GetSource()].Remove(new((edge.GetDestination())));
            }
        }
    }
}