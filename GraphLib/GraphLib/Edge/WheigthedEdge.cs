

using GraphLib.Vertex;

namespace GraphLib.Edge
{
    public class WheigthedEdge<TValue, TWheight> : Edge<TValue>
    {
        public TWheight Wheight { get; }
        
        public WheigthedEdge(Vertex<TValue> source, Vertex<TValue> destination, TWheight wheight) : base(source, destination)
        {
            Wheight = wheight;
        }
    }   
}

