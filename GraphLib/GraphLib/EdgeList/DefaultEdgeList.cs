using System.Collections;
using System.Collections.Generic;
using GraphLib.Propertys;
using GraphLib.Edge;
using GraphLib.Exceptions;


namespace GraphLib.EdgeList
{
    public class EdgeList<TVertex, TGraphType> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
    {
        private readonly Type graphType;
        private List<TVertex> vertex_list;
        private List<Edge<TVertex>> edge_list;
        public int Count { get { return vertex_list.Count; } }

        public EdgeList()
        {
            graphType = typeof(TGraphType);
            vertex_list = new List<TVertex>();
            edge_list = new List<Edge<TVertex>>();
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

        private void AddEdgeDirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Add(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            // TODO -> Levantar execao se os vertices nao existirem no grafo
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }
        
        private void RemoveEdgeDirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void RemoveEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Remove(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void RemoveEdge(Edge<TVertex> edge)
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

            if(vertex is null) throw new InvalidVertexException("A vertex can not be null");
            else if(vertex_list.IndexOf(vertex) == -1) throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            

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
