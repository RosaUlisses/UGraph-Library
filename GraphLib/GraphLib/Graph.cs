using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Edge;
using GraphLib.Propertys;

namespace GraphLib
{
    public abstract class Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        protected const  double EMPTY_EDGE = 0; 
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex vertex);
        public abstract void AddEdge(Edge<TVertex> edge);
        public abstract void RemoveEdge(Edge<TVertex> edge);
        public abstract int GetCount();
        public abstract IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex);

        public List<TVertex> BreadthFirstSearch(TVertex source)
        {
            List<TVertex> path  = new List<TVertex>();
            Queue<TVertex> queue = new Queue<TVertex>();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();

            queue.Enqueue(source);

            while(queue.Count != 0)
            {
                TVertex vertex = queue.Dequeue();
                if(!visitedVertexes.Contains(vertex))
                {
                    visitedVertexes.Add(vertex);
                    IEnumerator<OutEdge<TVertex>> neighbours = GetNeihgbours(vertex);
                    while(neighbours.MoveNext())
                    {
                        if(!visitedVertexes.Contains(neighbours.Current.Destination)) queue.Enqueue(neighbours.Current.Destination);
                    } 
                    path.Add(vertex);
                }
            }
            
            return path;
        }

        public List<TVertex> BreadthFirstSearch(TVertex source, TVertex destination)
        {
             LinkedList<TVertex> path  = new LinkedList<TVertex>();
             Queue<TVertex> queue = new Queue<TVertex>();
             HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
             Dictionary<TVertex, TVertex> predecesors = new Dictionary<TVertex, TVertex>();
             queue.Enqueue(source);

             while(queue.Count != 0 || visitedVertexes.Contains(destination))
             {
                 TVertex vertex = queue.Dequeue();
                 if(!visitedVertexes.Contains(vertex))
                 {
                     visitedVertexes.Add(vertex);
                     IEnumerator<OutEdge<TVertex>> neighbours = GetNeihgbours(vertex);
                     while(neighbours.MoveNext())
                     {
                         if (!visitedVertexes.Contains(neighbours.Current.Destination))
                         {
                             queue.Enqueue(neighbours.Current.Destination);
                             predecesors.Add(vertex, neighbours.Current.Destination);
                         }
                     } 
                 }
             }

             if (visitedVertexes.Contains(destination))
             {
                 TVertex current = destination;
                 while (!current.Equals(source))
                 {
                     path.AddFirst(current);
                     current = predecesors[current];
                 }
                 path.AddFirst(current);
             }

             return path.ToList();
        }

        public List<TVertex> DepthFirstSearch(TVertex source)
        {
            List<TVertex> path  = new List<TVertex>();
            Stack<TVertex> stack = new Stack<TVertex>();
            HashSet<TVertex> visitedVertexes = new HashSet<TVertex>();
            
            stack.Push(source);

            while (stack.Count != 0)
            {
                TVertex current = stack.Pop();
                visitedVertexes.Add(current);
                IEnumerator<OutEdge<TVertex>> neighbours = GetNeihgbours(current);
                while (neighbours.MoveNext())
                {
                    if (!visitedVertexes.Contains(neighbours.Current.Destination))
                    {
                        stack.Push(neighbours.Current.Destination);
                    }
                }
                path.Add(current);
            }
            return path;
        }
    }
}
