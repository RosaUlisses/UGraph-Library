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
        private HashSet<TVertex> vertexes;
        private List<Edge<TVertex>> edge_list;
        public int Count { get { return vertexes.Count; } }

        public EdgeList()
        {
            graphType = typeof(TGraphType);
            vertexes = new HashSet<TVertex>();
            edge_list = new List<Edge<TVertex>>();
        }

        public override void AddVertex(TVertex vertex)
        {
            vertexes.Add(vertex);
        }

        public override void RemoveVertex(TVertex vertex)
        {
            bool result;
            try
            {
                result = vertexes.Remove(vertex);
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
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWeight()));
        }

        private void AddEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWeight()));
            edge_list.Add(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWeight()));
        }

        public override void AddEdge(Edge<TVertex> edge)
        {
            if (edge.Source == null || edge.Destination == null)
            {
                throw new InvalidEdgeException($"Edge {edge} has invalid vertexes");
            }
            if (!vertexes.Contains(edge.Source) || !vertexes.Contains(edge.Destination))
            {
                throw new InvalidEdgeException($"Edge {edge} is not valid");
            }
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }
        
        private void RemoveEdgeDirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWeight()));
        }

        private void RemoveEdgeUndirectedGraph(Edge<TVertex> edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWeight()));
            edge_list.Remove(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWeight()));
        }

        public override void RemoveEdge(Edge<TVertex> edge)
        {
             if (edge.Source == null || edge.Destination == null)
             {
                 throw new InvalidEdgeException($"Edge {edge} has invalid vertexes");
             }
             if (!vertexes.Contains(edge.Source) || !vertexes.Contains(edge.Destination))
             {
                 throw new InvalidEdgeException($"Edge {edge} is not valid");
             }   
             if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
             else RemoveEdgeUndirectedGraph(edge);
        }

        public override bool AreConected(TVertex a, TVertex b)
        {
            if (a == null || b == null)
            {
                throw new InvalidEdgeException("A vertex can not be null");
            }
            if (!vertexes.Contains(a) || !vertexes.Contains(b))
            {
                throw new InvalidEdgeException("Invalid vertexes");
            }

            // Arrumar isso, ta horrivel
            return edge_list.Where(edge => edge.Source.Equals(a) && edge.Destination.Equals(b)).ToList().Count != 0;
        }

        public override bool Contains(TVertex vertex)
        {
            return vertexes.Contains(vertex);
        }

        public override int GetCount()
        {
            return Count;
        }

        public override IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex)
        {
            List<OutEdge<TVertex>> neighbours = new List<OutEdge<TVertex>>();

            if(vertex is null) throw new InvalidVertexException("A vertex can not be null");
            if(!vertexes.Contains(vertex)) throw new InvalidVertexException($"Vertex {vertex} does not exist in the graph");
            

            foreach (Edge<TVertex> edge in edge_list)
            {
                if (edge.Source.Equals(vertex))
                {
                   neighbours.Add(new OutEdge<TVertex>(edge.Destination, edge.Weight)); 
                } 
            }
            return neighbours.GetEnumerator();
        }
    }
}
