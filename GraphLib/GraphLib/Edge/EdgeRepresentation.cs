using GraphLib.Vertex;

namespace GraphLib.Edge
{
    public class EdgeRepresentation<T>
    {
        public Vertex<T> Destination { get; }

        public EdgeRepresentation(Vertex<T> destination)
        {
            Destination = destination;
        }
    }   
}