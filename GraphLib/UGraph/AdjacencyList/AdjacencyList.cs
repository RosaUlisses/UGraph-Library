﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UGraph.Exceptions;
using UGraph.Propertys;

namespace UGraph.AdjacencyList
{
    public class AdjacencyList<TVertex, TGraphType> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        private readonly Type graphType;
        private Dictionary<TVertex, List<OutEdge<TVertex>>> adjacency_lists;

        private IEnumerator<TVertex> current_vertex;

        public int Count
        {
            get { return adjacency_lists.Count; }
        }

        public AdjacencyList()
        {
            adjacency_lists = new Dictionary<TVertex, List<OutEdge<TVertex>>>();
            graphType = typeof(TGraphType);
            current_vertex = null;
        }

        public AdjacencyList(Graph<TVertex, TGraphType> graph)
        {
            if (graph is null)
            {
                throw new InvalidGraphException($"Graph {nameof(graph)} is null");
            }

            adjacency_lists = new Dictionary<TVertex, List<OutEdge<TVertex>>>();
            graphType = typeof(TGraphType);
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
            current_vertex = adjacency_lists.Keys.GetEnumerator();
        }

        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            try
            {
                adjacency_lists.Add(vertex, new List<OutEdge<TVertex>>());
            }
            catch (ArgumentException e)
            {
                throw new InvalidEdgeException($"Vertex {nameof(vertex)} does not exist in the graph");
            }
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
                throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            }

            if (!result)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }
        }

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.Source].Add(new(edge.Destination, edge.GetWeight()));
            adjacency_lists[edge.Destination].Add(new(edge.Source, edge.GetWeight()));
        }

        private void AddEdgeDirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.Source].Add(new(edge.Destination, edge.GetWeight()));
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            try
            {
                if (adjacency_lists[edge.Source].Contains(new OutEdge<TVertex>(edge.Destination, edge.Weight)))
                {
                    adjacency_lists[edge.Source].Remove(new OutEdge<TVertex>(edge.Destination));
                    if (graphType == typeof(Undirected))
                        adjacency_lists[edge.Destination].Remove(new OutEdge<TVertex>(edge.Source));
                }

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
            adjacency_lists[edge.Source].Remove(new(edge.Destination, edge.GetWeight()));
            adjacency_lists[edge.Destination].Remove(new(edge.Source, edge.GetWeight()));
        }

        private void RemoveEdgeDirectedGraph(Edge<TVertex> edge)
        {
            adjacency_lists[edge.Source].Remove(new(edge.Destination, edge.GetWeight()));
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
            try
            {
                return adjacency_lists[vertex].GetEnumerator();
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"The source vertex {nameof(vertex)} is null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"The source vertex {nameof(vertex)} does not exist in the graph");
            }
        }

        protected override Graph<TVertex, TGraphType> GetTransposedGraph()
        {
            Graph<TVertex, TGraphType> transposedGraph = new AdjacencyList<TVertex, TGraphType>();
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
            try
            {
                return adjacency_lists[vertex].Select(value => value.Destination).ToList();
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException($"The source vertex {nameof(vertex)} is null");
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidVertexException($"The source vertex {nameof(vertex)} does not exist in the graph");
            }
        }
    }
}