using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class WheightedOutEdge<TVertex,TWheight> : OutEdge<TVertex, TWheight>
    {
        public TWheight Wheight { get; } 
        
        public WheightedOutEdge(IEdge<TVertex, TWheight> edge) : base(edge)
        {
            Wheight = edge.GetWheight();
        }
    }   
}