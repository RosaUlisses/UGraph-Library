using UGraph.Edge;

namespace UGraph.Edge
{
    public class OutEdge<TVertex> : IComparable<OutEdge<TVertex>>
    where TVertex : IComparable<TVertex>
    {
        public TVertex Destination { get; }
        public double Weight { get; }
        
        public OutEdge(TVertex vertex, double weight)
        {
            Destination = vertex; 
            Weight = weight;
        }
        
        public OutEdge(TVertex vertex)
        {
            Destination = vertex; 
            Weight = 1;
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

