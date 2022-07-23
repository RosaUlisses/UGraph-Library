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
    public class AdjacencyList<TVertex, TGraphType, TList, TMap> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
        where TList : ICollection<OutEdge<TVertex>>, new()
        where TMap : IDictionary<TVertex, TList>, new()
    {
        private readonly Type graphType;
        private IDictionary<TVertex, TList> adjacency_lists;
        
        public int Count { get { return adjacency_lists.Count; } }

        private IEnumerator<TVertex> current_vertex;
        private bool IsOccuringAIteration;
        public AdjacencyList()
        {
            graphType = typeof(TGraphType);
            adjacency_lists = new TMap();
            IsOccuringAIteration = false;
        }
        
        public override bool MoveIterator()
        {
            if (!IsOccuringAIteration)
            {
                ResetIterator();
                IsOccuringAIteration = true;
            }

            if (!current_vertex.MoveNext())
            {
                IsOccuringAIteration = false;
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
            current_vertex = adjacency_lists.Keys.GetEnumerator();
        } 
        
        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException("A vertex can not be null");
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

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWeight())); 
            adjacency_lists[edge.GetDestination()].Add(new(edge.GetSource(), edge.GetWeight()));
        }

        private void AddEdgeEdgeDirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWeight())); 
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            try
            {
                 if (graphType == typeof(Directed)) AddEdgeEdgeDirectedGraph(edge);
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
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWeight())); 
            adjacency_lists[edge.GetDestination()].Remove(new(edge.GetSource(), edge.GetWeight()));
        }

        private void RemoveEdgeDirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWeight())); 
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
            return adjacency_lists.ContainsKey(vertex);
        }

        public override bool AreConected(TVertex a, TVertex b)
        {
            try
            {
                return adjacency_lists[a].Contains(new OutEdge<TVertex>(b));
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