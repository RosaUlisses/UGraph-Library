﻿using System;
using System.Collections;
using System.Collections.Generic;
using UGraph.Edge;
using UGraph.EdgeList;
using UGraph.Propertys;

namespace UGraph
{
    public abstract class Graph<TVertex, TGraphType> : IEnumerator, IEnumerable
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        
        protected const  double EMPTY_EDGE = 0;

        public bool MoveNext()
        {
            return MoveIterator();
        }

        public void Reset()
        {
            ResetIterator();
        }

        public object Current
        {
            get { return GetIteratorValue(); }
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public abstract void ResetIterator();
        public abstract bool MoveIterator();
        public abstract TVertex GetIteratorValue();
        
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex vertex);
        public abstract void AddEdge(Edge<TVertex> edge);
        public abstract void UpdateEdgeWeight(Edge<TVertex> edge, double weight);
        public abstract void RemoveEdge(Edge<TVertex> edge);
        public abstract void ClearEdges();
        public abstract bool Contains(TVertex vertex);
        public abstract bool AreConnected(TVertex a, TVertex b);
        public abstract int GetCount();
        protected abstract IEnumerator<OutEdge<TVertex>> GetAdjacentVertexes(TVertex vertex);

        public abstract List<TVertex> GetAdjacencyList(TVertex vertex);
        
        
        public List<TVertex> BreadthFirstSearch(TVertex source)
        {
            List<TVertex> path  = new List<TVertex>();
            Queue<TVertex> queue = new Queue<TVertex>();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();

            queue.Enqueue(source);

            while(queue.Count > 0)
            {
                TVertex vertex = queue.Dequeue();
                visitedVertexes.Add(vertex);
                IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(vertex);
                while(adjacents.MoveNext())
                {
                    if(!visitedVertexes.Contains(adjacents.Current.Destination)) queue.Enqueue(adjacents.Current.Destination);
                } 
                path.Add(vertex);
            }
            
            return path;
        }

        private List<TVertex> GetPathFromPredecesorMap(TVertex source, TVertex destination, Dictionary<TVertex, TVertex> predecessors)
        {
            LinkedList<TVertex> path = new LinkedList<TVertex>();
            TVertex current = destination;
            while (!current.Equals(source))
            {
                path.AddFirst(current);
                current = predecessors[current];
            }
            path.AddFirst(current);
            return path.ToList();
        }

        public List<TVertex> BreadthFirstSearch(TVertex source, TVertex destination)
        {
             Queue<TVertex> queue = new Queue<TVertex>();
             HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
             Dictionary<TVertex, TVertex> predecessors = new Dictionary<TVertex, TVertex>();
             queue.Enqueue(source);
             List<TVertex> path  = new List<TVertex>();

             while(queue.Count > 0 || !visitedVertexes.Contains(destination))
             {
                 TVertex vertex = queue.Dequeue();
                 visitedVertexes.Add(vertex);
                 IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(vertex);
                 while(adjacents.MoveNext())
                 {
                     if (!visitedVertexes.Contains(adjacents.Current.Destination))
                     {
                         queue.Enqueue(adjacents.Current.Destination);
                         predecessors.Add(adjacents.Current.Destination, vertex);
                     }
                 } 
             }

             if (visitedVertexes.Contains(destination))
             {
                 path = GetPathFromPredecesorMap(source, destination, predecessors);
             }
             return path;
        }

        public List<TVertex> DepthFirstSearch(TVertex source)
        {
            List<TVertex> path  = new List<TVertex>();
            Stack<TVertex> stack = new Stack<TVertex>();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
            
            stack.Push(source);

            while (stack.Count > 0)
            {
                TVertex current = stack.Pop();
                visitedVertexes.Add(current);
                IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(current);
                while (adjacents.MoveNext())
                {
                    if (!visitedVertexes.Contains(adjacents.Current.Destination))
                    {
                        stack.Push(adjacents.Current.Destination);
                    }
                }
                path.Add(current);
            }
            return path;
        }

        public List<Tuple<TVertex,int,int>> DepthFirstSearch()
        {
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
            Dictionary<TVertex, Tuple<int, int>> times = new Dictionary<TVertex, Tuple<int, int>>();

            int time = 0;
            foreach (TVertex vertex in this)
            {
                if (!visitedVertexes.Contains(vertex))
                {
                    DetphFirstSearchVisit(vertex, visitedVertexes, times, ref time);
                }
            }

            List<Tuple<TVertex, int, int>> timesList = new List<Tuple<TVertex, int, int>>();
            foreach (KeyValuePair<TVertex, Tuple<int,int>> pair in times)
            {
                timesList.Add(new Tuple<TVertex, int, int>(pair.Key, pair.Value.Item1, pair.Value.Item2)); 
            }

            return timesList;
        }

        private void DetphFirstSearchVisit(TVertex vertex, HashSet<TVertex> visitedVertexes, Dictionary<TVertex, Tuple<int,int>> times, ref int time)
        {
            int firstTime = time + 1;
            visitedVertexes.Add(vertex);
            IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(vertex);

            while (adjacents.MoveNext())
            {
                if (!visitedVertexes.Contains(adjacents.Current.Destination))
                {
                    DetphFirstSearchVisit(adjacents.Current.Destination, visitedVertexes, times, ref time);
                }
            }
            int secondTime = time + 1;
            times[vertex] = new Tuple<int, int>(firstTime, secondTime);
        }

        public List<TVertex> TopologicalSort()
        { 
            // Levantar excecao se o grafo nao for um DAG (direcionado aciclico) ???? 
            List<Tuple<TVertex, int, int>> timesDFS = DepthFirstSearch();
            // Rever esse return porque esta ruim !!!!
            return timesDFS
                .OrderBy(value => value.Item3)
                .Select(value => value.Item1)
                .ToList();
        }

        public IEnumerator<Edge<TVertex>> GetAllEdges()
        {
            List<Edge<TVertex>> edges = new List<Edge<TVertex>>();
            foreach (TVertex vertex in this)
            {
                IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(vertex);
                while (adjacents.MoveNext())
                {
                    edges.Add(new Edge<TVertex>(vertex, adjacents.Current.Destination, adjacents.Current.Weight)); 
                }
            }

            return edges.GetEnumerator();
        }

        private Dictionary<TVertex, double> InitDistanceMap()
        {
            Dictionary<TVertex, double> map = new Dictionary<TVertex, double>();
            foreach (TVertex vertex in this)
            {
                map.Add(vertex, Double.PositiveInfinity);
            }

            return map;
        }

        private void RelaxEdge(Edge<TVertex> edge, Dictionary<TVertex, double> distances, Dictionary<TVertex, TVertex> predecessors)
        {
            if (distances[edge.Destination] > distances[edge.Source] + edge.Weight)
            {
                distances[edge.Destination] = distances[edge.Source] + edge.Weight;
                predecessors[edge.Destination] = edge.Source;
            }
        }
        
        public List<TVertex> Dijkstra(TVertex source, TVertex destination)
        {
            PriorityQueue<TVertex, double> queue = new PriorityQueue<TVertex, double>();
            Dictionary<TVertex, double> distances = InitDistanceMap();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
            Dictionary<TVertex, TVertex> predecessors = new Dictionary<TVertex, TVertex>();
            List<TVertex> path = new List<TVertex>();
            
            distances[source] = 0;
            queue.Enqueue(source, 0);
            while (queue.Count > 0 || !visitedVertexes.Contains(destination))
            {
                TVertex current = queue.Dequeue();
                while (visitedVertexes.Contains(current))
                {
                    current = queue.Dequeue();
                }

                IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(current);
                while (adjacents.MoveNext())
                {
                    double previousDistance = distances[adjacents.Current.Destination];        
                    RelaxEdge(new Edge<TVertex>(current, adjacents.Current.Destination,
                        adjacents.Current.Weight), distances, predecessors);
                    if (distances[current] + distances[adjacents.Current.Destination] < previousDistance)
                    {
                        queue.Enqueue(adjacents.Current.Destination, distances[current] + distances[adjacents.Current.Destination]);
                    }
                }
                visitedVertexes.Add(current);
            }
            
            if (visitedVertexes.Contains(destination))
            {
                 path = GetPathFromPredecesorMap(source, destination, predecessors);
            }
            return path;
        }

        public List<TVertex> BellmanFord(TVertex source, TVertex destination)
        {
            Dictionary<TVertex, double> distances = InitDistanceMap();
            Dictionary<TVertex, TVertex> predecessors = new Dictionary<TVertex, TVertex>();
            List<TVertex> path = new List<TVertex>();

            distances[source] = 0;
            for (int i = 0; i < this.GetCount(); i++)
            {
                IEnumerator<Edge<TVertex>> edges = GetAllEdges();
                while (edges.MoveNext())
                {
                    RelaxEdge(edges.Current, distances, predecessors);
                }
            }

            if (predecessors.ContainsKey(destination))
            {
                path = GetPathFromPredecesorMap(source, destination, predecessors);
            }

            return path;
        }

        public bool HasNegativeCycles(TVertex source)
        {
            Dictionary<TVertex, double> distances = InitDistanceMap();
            Dictionary<TVertex, TVertex> predecessors = new Dictionary<TVertex, TVertex>();
            List<TVertex> path = new List<TVertex>();
            IEnumerator<Edge<TVertex>> edges;
            distances[source] = 0;
            for (int i = 0; i < this.GetCount(); i++)
            {
                 edges = GetAllEdges();
                 while (edges.MoveNext())
                 {
                     RelaxEdge(edges.Current, distances, predecessors);
                 }
            }

            edges = GetAllEdges();
            while (edges.MoveNext())
            {
                if (distances[edges.Current.Destination] < distances[edges.Current.Source] + edges.Current.Weight)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasCycle(TVertex source)
        {
            Stack<TVertex> stack = new Stack<TVertex>();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();

            stack.Push(source);

            while (stack.Count > 0)
            {
                 TVertex current = stack.Pop();
                 visitedVertexes.Add(current);
                 IEnumerator<OutEdge<TVertex>> adjacents = GetAdjacentVertexes(current);
                 while (adjacents.MoveNext())
                 {
                     if (visitedVertexes.Contains(adjacents.Current.Destination))
                     {
                         return true;
                     }
                     stack.Push(adjacents.Current.Destination);
                 } 
            }
            return false;
        }

        public bool HasCycle()
        {
            foreach (TVertex vertex in this)
            {
                if (HasCycle(vertex))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
    
}
