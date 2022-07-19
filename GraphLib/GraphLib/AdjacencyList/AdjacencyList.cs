using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.Exceptions;

namespace GraphLib.AdjacencyList
{
    public class AdjacencyList<TVertex, TEdge, TGraphType, TList, TMap> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TEdge : IEdge<TVertex>
        where TGraphType : GraphType
        where TList : ICollection<OutEdge<TVertex>>, new()
        where TMap : IDictionary<TVertex, TList>, new()
    {
        private readonly Type graphType;
        // TMap ou IDictionary ??? (Redundancia ???)
        private IDictionary<TVertex, TList> adjacency_lists;
        
        public int Count { get { return adjacency_lists.Count; } }

        public AdjacencyList()
        {
            graphType = typeof(TGraphType);
            adjacency_lists = new TMap();
        }

        public override void AddVertex(TVertex vertex)
        {
            adjacency_lists.Add(vertex, new TList()); 
        }

        public override void RemoveVertex(TVertex vertex)
        {
            bool result;
            try
            {
                result = adjacency_lists.Remove(vertex);
            }            
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }
            if (!result)
            {
                throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            }
        }

        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWheight())); 
            adjacency_lists[edge.GetDestination()].Add(new(edge.GetSource(), edge.GetWheight()));
        }

        private void AddEdgeEdgeDirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWheight())); 
        }

        public override void AddEdge(TEdge edge)
        {
            if (graphType == typeof(Directed)) AddEdgeEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }

        private void RemoveEdgeUndirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWheight())); 
            adjacency_lists[edge.GetDestination()].Remove(new(edge.GetSource(), edge.GetWheight()));
        }

        private void RemoveEdgeDirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWheight())); 
        }

        public override void RemoveEdge(TEdge edge)
        {
            try
            {
                if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
                else RemoveEdgeUndirectedGraph(edge);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"Edge {edge} does not exist in the graph");
            }
        }

        public override int GetCount()
        {
            return Count;
        }

        public override IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex)
        {
            try
            {
                return adjacency_lists[vertex].GetEnumerator();
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