namespace GraphLib
{
    public interface IGraph<TVertex>
    {
        List<TVertex> BreadthFirstSearch(TVertex source);
        List<TVertex> BreadthFirstSearch(TVertex source, TVertex destination);
        List<TVertex> DepthFirstSearch(TVertex source);
        List<TVertex> DepthFirstSearch(TVertex source, TVertex destination);
        List<TVertex> TopologicalSort(TVertex source);
        List<TVertex> Djikstra(TVertex source, TVertex destination);
        List<TVertex> BellmanFord(TVertex source, TVertex destination);
        List<TVertex> AStar(TVertex source, TVertex destination);
    }   
}

