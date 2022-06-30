

using GraphLib.Vertex;

namespace GraphLib.Edge
{
    public class WheigthedEdgeRepresentation<TValue, TWheight> : EdgeRepresentation<TValue>
    {
        public TWheight Wheight { get; }
        
        public WheigthedEdgeRepresentation(Vertex<TValue> destination, TWheight wheight) : base(destination)
        {
            Wheight = wheight;
        }
    }   
}

