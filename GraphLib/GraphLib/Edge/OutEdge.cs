using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class OutEdge<TVertex>
    {
        public TVertex Destination { get; }
        public double Wheight { get; }
        
        public OutEdge(IEdge<TVertex> edge)
        {
            // TODO -> levantar execao caso edge seja null
            Destination = edge.GetDestination();
            Wheight = edge.GetWheight();
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null  || GetType().Equals(obj.GetType()))
            {
                return false;
            }
            OutEdge<TVertex> outEdge = (OutEdge<TVertex>)obj;
            return Destination.Equals(outEdge.Destination) && Wheight == outEdge.Wheight;
        }

        // TODO -> fazer uma funcao hash boa
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }   
}

