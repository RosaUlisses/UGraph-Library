using GraphLib.Vertex;

namespace GraphLib.Edge
{
    public class Edge<T>
    {
        public Vertex<T> Source { get; }
        public Vertex<T> Destination { get; }

        public Edge(Vertex<T> source, Vertex<T> destination)
        {
            Source = source;
            Destination = destination;
        }
    }   
}

