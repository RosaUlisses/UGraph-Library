using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class WheightedOutEdge<TVertex, TWheight> : OutEdge<TVertex>
    {
        public TWheight Wheight { get; }

        public bool HasWheight()
        {
            return true;
        } 
        
        public WheightedOutEdge(IEdge<TVertex, TWheight> edge) : base(edge.GetDestination())
        {
            Wheight = edge.GetWheight();
        }
    }   
}