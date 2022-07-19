using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex;
using GraphLib.Edge;
using GraphLib.Propertys;

namespace GraphLib
{
    public abstract class Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TEdge : IEdge<TVertex>
        where TGraphType : GraphType
    {
        protected const  double EMPTY_EDGE = 0; 
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex vertex);
        public abstract void AddEdge(TEdge edge);
        public abstract void RemoveEdge(TEdge edge);
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
    }
}
