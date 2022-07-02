namespace GraphLib.Edge
{
    public class Edge<TVertex, TWheight> : IEdge<TVertex, TWheight>
    {
        public TVertex Source { get; }
        public TVertex Destination { get; }
        public TWheight Wheight { get; }

        public Edge(TVertex source, TVertex destination)
        {
            Source = source;
            Destination = destination;
            Wheight = default(TWheight);
        }

        public Edge(TVertex source, TVertex destination, TWheight wheight)
        {
            Source = source;
            Destination = destination;
            Wheight = wheight;
        }

        public bool HasWeight()
        {
            return false;
        }

        public TWheight GetWheight()
        {
            return Wheight;
        }

        public TVertex GetSource()
        {
            return Source;
        }

        public TVertex GetDestination()
        {
            return Destination;
        }
    }   
}

