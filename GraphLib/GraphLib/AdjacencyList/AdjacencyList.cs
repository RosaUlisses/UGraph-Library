using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphLib.Edge;
using GraphLib.Vertex;

namespace GraphLib.AdjacencyList
{
    public class AdjacencyList<TVertex, TEdge, TGraphType, TMap, TList> : Graph<TVertex, TEdge, TGraphType>
        where TEdge : IEdge<TVertex>
        where TMap : Propertys.MapCollection
        where TList : Propertys.Collection 
        
    {
        private IDictionary<TVertex, ICollection<OutEdge<TVertex>>> adjacency_lists;
        public int Count { get; private set; }

        public AdjacencyList()
        {
            if(typeof(TMap) is Propertys.Map)
            {
                adjacency_lists = new Dictionary<TVertex, ICollection<OutEdge<TVertex>>>();
            }
            else if(typeof(TMap) is Propertys.SortedMap)
            {
                adjacency_lists = new SortedDictionary<TVertex, ICollection<OutEdge<TVertex>>>();
            }
            else if(typeof(TMap) is Propertys.SortedList)
            {
                adjacency_lists = new SortedList<TVertex, ICollection<OutEdge<TVertex>>>();
            }
            Count = 0;
        }

        public override void AddVertex(TVertex vertex)
        {
            if(typeof(TList) is Propertys.Vector)
            {
                adjacency_lists.Add(vertex, new List<OutEdge<TVertex>>());
            }
            else if(typeof(TList) is Propertys.LinkedList)
            {
                adjacency_lists.Add(vertex, new LinkedList<OutEdge<TVertex>>());
            }
            else if (typeof(TList) is Propertys.Set)
            {
                adjacency_lists.Add(vertex, new HashSet<OutEdge<TVertex>>());
            }
            else if (typeof(TList) is Propertys.Set)
            {
                adjacency_lists.Add(vertex, new SortedSet<OutEdge<TVertex>>());
            }
            Count++;
        }

        public override void RemoveVertex(TVertex vertex)
        {
            // TODO -> levantar execao caso vertex seja null 
            // TODO -> levantar execao caso o vertice nao exista
            // TODO -> levantar execao caso o o grafo esteja vazio

        }

        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
        }

        public override void RemoveEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
        }
    }
}