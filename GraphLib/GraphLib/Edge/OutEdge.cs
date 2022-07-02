using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class OutEdge<TVertex,TWheight>
    {
        public TVertex Destination { get; }

        public OutEdge(IEdge<TVertex, TWheight> edge)
        {
            Destination = edge.GetDestination();
        }
    }   
}

