namespace GraphLib.Edge
{
   public interface IEdge<TVertex>
   {
      public TVertex GetSource();
      public TVertex GetDestination();
      public double GetWheight();
   }
}