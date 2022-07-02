using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class OutEdge<TVertex>
    {
        public TVertex Destination { get; }

        public bool HasWheight()
        {
            return false;
        }

        public OutEdge(TVertex destination)
        {
            destination = destination;
        }
    }   
}

