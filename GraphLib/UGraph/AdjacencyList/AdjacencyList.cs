using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UGraph.Edge;
using UGraph.EdgeList;
using UGraph.Propertys;
using UGraph.Exceptions;

namespace UGraph.AdjacencyList
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
        public AdjacencyList()
        {
            graphType = typeof(TGraphType);
            adjacency_lists = new TMap();
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

        private bool UpdateEdgeWeightDirectedGraph(Edge<TVertex> edge, double weight)
        {
            bool result = adjacency_lists[edge.Source].Remove(new OutEdge<TVertex>(edge.Destination));
            if (result)
            {
                adjacency_lists[edge.Source].Add(new OutEdge<TVertex>(edge.Destination, weight));
            }

            return result;
        }
        
        private bool UpdateEdgeWeightUndirectedGraph(Edge<TVertex> edge, double weight)
        {
             bool result = adjacency_lists[edge.Source].Remove(new OutEdge<TVertex>(edge.Destination)) 
                           && adjacency_lists[edge.Destination].Remove(new OutEdge<TVertex>(edge.Source, weight));
             if (result)
             {
                 adjacency_lists[edge.Source].Add(new OutEdge<TVertex>(edge.Destination, weight));
                 adjacency_lists[edge.Destination].Add(new OutEdge<TVertex>(edge.Source, weight));
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

        public override void ClearEdges()
        {
            foreach (TVertex vertex in this)
            {
               adjacency_lists[vertex].Clear();
            }
        }

        public override bool Contains(TVertex vertex)
        {
            return adjacency_lists.ContainsKey(vertex);
        }

        public override bool AreConnected(TVertex a, TVertex b)
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

        protected override IEnumerator<OutEdge<TVertex>> GetAdjacentVertexes(TVertex vertex)
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

        public override List<TVertex> GetAdjacencyList(TVertex vertex)
        {
            try
            {
                return adjacency_lists[vertex].Select(value => value.Destination).ToList();
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