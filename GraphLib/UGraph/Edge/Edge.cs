using System.Net.Sockets;

namespace UGraph.Edge
{
    public class Edge<TVertex> :  IEdge<TVertex>
    {
        public TVertex Source { get; }
        public TVertex Destination { get; }
        public double Weight { get; }

        public Edge(TVertex source, TVertex destination, double weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public Edge(TVertex source, TVertex destination)
        {
            Source = source;
            Destination = destination;
            Weight = 1;
        }

        public TVertex GetSource()
        {
            return Source;
        }

        public TVertex GetDestination()
        {
            return Destination;
        }

        public double GetWeight()
        {
            return Weight;
        }
    }   
}

