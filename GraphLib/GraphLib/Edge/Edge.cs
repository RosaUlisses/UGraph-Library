using System.Net.Sockets;

namespace GraphLib.Edge
{
    public class Edge<TVertex> :  IEdge<TVertex>
    {
        public TVertex Source { get; }
        public TVertex Destination { get; }
        public double Wheight { get; }

        public Edge(TVertex source, TVertex destination, double wheight)
        {
            Source = source;
            Destination = destination;
            Wheight = wheight;
        }

        public Edge(TVertex source, TVertex destination)
        {
            Source = source;
            Destination = destination;
            Wheight = 1;
        }

        public TVertex GetSource()
        {
            return Source;
        }

        public TVertex GetDestination()
        {
            return Destination;
        }

        public double GetWheight()
        {
            return Wheight;
        }
    }   
}

