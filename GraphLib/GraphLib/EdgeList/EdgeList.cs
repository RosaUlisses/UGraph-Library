using System.Collections;
using System.Collections.Generic;
using GraphLib.Propertys;
using GraphLib.Edge;
using GraphLib.Exceptions;


namespace GraphLib.EdgeList
{
    public class EdgeList<TVertex, TEdge, TGraphType, TVertexList, TEdgeList> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TEdge : IEdge<TVertex>
        where TGraphType : GraphType
        where TVertexList : ICollection<TVertex>, new()
        where TEdgeList : ICollection<Edge<TVertex>>, new()
    {
        private readonly Type graphType;
        private TVertexList vertex_list;
        private TEdgeList edge_list;
        public int Count { get { return vertex_list.Count; } }

        public EdgeList()
        {
            graphType = typeof(TGraphType);
            vertex_list = new TVertexList();
            edge_list = new TEdgeList();
        }
        

        public override void AddVertex(TVertex vertex)
        {
            vertex_list.Add(vertex);
        }

        public override void RemoveVertex(TVertex vertex)
        {
            bool result;
            try
            {
                result = vertex_list.Remove(vertex);
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidVertexException("A vertex can not be null");
            }

            if (!result)
            {
                throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            }
        }

        private void AddEdgeDirectedGraph(TEdge edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Add(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void AddEdge(TEdge edge)
        {
            // TODO -> Levantar execao se os vertices nao existirem no grafo
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }
        
        private void RemoveEdgeDirectedGraph(TEdge edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void RemoveEdgeUndirectedGraph(TEdge edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Remove(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void RemoveEdge(TEdge edge)
        {
            // TODO -> levantar execao se a aresta nao existir no grafo
            // Considerar o peso na hora de remover a aresta ???
             if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
             else RemoveEdgeUndirectedGraph(edge);
                
        }

        public override int GetCount()
        {
            return Count;
        }

        public override IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex)
        {
            List<OutEdge<TVertex>> neighbours = new List<OutEdge<TVertex>>();

            foreach (Edge<TVertex> edge in edge_list)
            {
                if (edge.Source.Equals(vertex))
                {
                   neighbours.Add(new OutEdge<TVertex>(edge.Destination, edge.Wheight)); 
                } 
            }
            return neighbours.GetEnumerator();
        }
    }
}