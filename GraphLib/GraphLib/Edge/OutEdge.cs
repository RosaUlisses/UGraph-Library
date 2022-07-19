using GraphLib.Edge;

namespace GraphLib.Edge
{
    public class OutEdge<TVertex> : IComparable<OutEdge<TVertex>>
    where TVertex : IComparable<TVertex>
    {
        public TVertex Destination { get; }
        public double Weight { get; }
        
        public OutEdge(TVertex vertex, double weight)
        {
            // TODO -> levantar execao caso edge seja null
            Destination = vertex; 
            Weight = weight;
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null  || GetType().Equals(obj.GetType()))
            {
                return false;
            }
            OutEdge<TVertex> outEdge = (OutEdge<TVertex>)obj;
            return Destination.Equals(outEdge.Destination) && Weight == outEdge.Weight;
        }

        // TODO -> fazer uma funcao hash boa
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(OutEdge<TVertex>? other)
        {
            int result = Destination.CompareTo(other.Destination);
            if (result < 0) return -1;
            if (result > 0) return 1;

            if (Weight < other.Weight) return -1;
            if (Weight > other.Weight) return 1;

            return 0;
        }
    }   
}

