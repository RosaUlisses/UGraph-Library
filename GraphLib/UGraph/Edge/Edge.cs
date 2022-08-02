using System.Net.Sockets;
using System.Security.Cryptography;

namespace UGraph.Edge
{
    public class Edge<TVertex> : IEdge<TVertex>
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

        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Edge<TVertex> edge = (Edge<TVertex>)obj;
            return Source.Equals(edge.Source) && Destination.Equals(edge.Destination);
        }

        public override int GetHashCode()
        {
            int a = Source.GetHashCode();
            int b = Destination.GetHashCode();
            int A = a >= 0 ? 2 * a : -2 * a - 1;
            int B = b >= 0 ? 2 * b : -2 * b - 1;
            int C = (A >= B ? A * A + A + B : A + B * B) / 2;
            return a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1; 
        }
    }
}