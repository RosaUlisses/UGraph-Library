namespace GraphLib.Edge
{
   public interface IEdge<TVertex, TWeight>
   {
      public bool HasWeight();
      public TWeight GetWheight();

      public TVertex GetSource();

      public TVertex GetDestination();
   }
}