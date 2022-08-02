using System.Collections;
using System.Collections.Generic;
using UGraph.Propertys;
using UGraph.Edge;
using UGraph.Exceptions;


namespace UGraph.EdgeList
{
    public class EdgeList<TVertex, TGraphType, TVertexList, TEdgeList> : Graph<TVertex, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
        where TVertexList : ICollection<TVertex>, new()
        where TEdgeList : ICollection<Edge<TVertex>>, new()
    {
        private readonly Type graphType;
        private TVertexList vertexes;
        private TEdgeList edge_list;

        public int Count
        {
            get { return vertexes.Count; }
        }

        private IEnumerator<TVertex> current_vertex;

        public EdgeList()
        {
            graphType = typeof(TGraphType);
            vertexes = new TVertexList();
            edge_list = new TEdgeList();
            current_vertex = null;
        }

        public override bool MoveIterator()
        {
            if (current_vertex is null)
            {
                ResetIterator();
            }

            if (!current_vertex.MoveNext())
            {
                current_vertex = null;
                return false;
            }

            return true;
        }

        public override TVertex GetIteratorValue()
        {
            return current_vertex.Current;
        }

        public override void ResetIterator()
        {
            current_vertex = vertexes.GetEnumerator();
        }

        public override void AddVertex(TVertex vertex)
        {
            if (vertex is null) throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            if (vertexes.Contains(vertex))
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} already exists in the graph");
            }

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
                throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            }

            if (!result)
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
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
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            if (edge.Source is null || edge.Destination is null)
            {
                throw new InvalidEdgeException($"One or both vertexes of edge {nameof(edge)} are null");
            }

            if (!vertexes.Contains(edge.Source) || !vertexes.Contains(edge.Destination))
            {
                throw new InvalidEdgeException(
                    $"One or both vertexes of edge {nameof(edge)} do not exist in the graph");
            }

            if (edge_list.Contains(edge))
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} already exists in the graph");
            }

            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }

        private bool UpdateEdgeWeightDirectedGraph(Edge<TVertex> edge, double weight)
        {
            bool result = edge_list.Remove(edge);
            if (result)
            {
                edge_list.Add(new Edge<TVertex>(edge.Source, edge.Destination, weight));
            }

            return result;
        }

        private bool UpdateEdgeWeightUndirectedGraph(Edge<TVertex> edge, double weight)
        {
            bool result = edge_list.Remove(edge) && edge_list.Remove(edge);
            if (result)
            {
                edge_list.Add(new Edge<TVertex>(edge.Source, edge.Destination, weight));
                edge_list.Add(new Edge<TVertex>(edge.Destination, edge.Source, weight));
            }

            return result;
        }

        public override void UpdateEdgeWeight(Edge<TVertex> edge, double weight)
        {
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            if (edge.Source is null || edge.Destination is null)
            {
                throw new InvalidEdgeException($"One or both vertexes of edge {nameof(edge)} are null");
            }

            if (!vertexes.Contains(edge.Source) || !vertexes.Contains(edge.Destination))
            {
                throw new InvalidEdgeException(
                    $"One or both vertexes of edge {nameof(edge)} do not exist in the graph");
            }

            bool result;
            if (graphType == typeof(Directed))
            {
                result = UpdateEdgeWeightDirectedGraph(edge, weight);
            }
            else
            {
                result = UpdateEdgeWeightUndirectedGraph(edge, weight);
            }

            if (!result)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} does not exist in the graph");
            }
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
            if (edge is null)
            {
                throw new InvalidEdgeException($"Edge {nameof(edge)} is null");
            }

            if (edge.Source is null || edge.Destination is null)
            {
                throw new InvalidEdgeException($"One or both vertexes of edge {nameof(edge)} are null");
            }

            if (!vertexes.Contains(edge.Source) || !vertexes.Contains(edge.Destination))
            {
                throw new InvalidEdgeException(
                    $"One or both vertexes of edge {nameof(edge)} do not exist in the graph");
            }

            if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
            else RemoveEdgeUndirectedGraph(edge);
        }

        public override void ClearEdges()
        {
            edge_list.Clear();
        }

        public override bool Contains(TVertex vertex)
        {
            return vertexes.Contains(vertex);
        }

        public override bool AreConnected(TVertex a, TVertex b)
        {
            if (a is null || b is null)
            {
                throw new InvalidVertexException($"One or both of vertexes {a} and {b} are null");
            }

            if (!vertexes.Contains(a) || !vertexes.Contains(b))
            {
                throw new InvalidVertexException($"One or both of vertexes {a} and {b} do not exist in the graph");
            }

            return edge_list.Where(edge => edge.Source.Equals(a) && edge.Destination.Equals(b)).ToList().Count != 0;
        }

        public override int GetCount()
        {
            return Count;
        }

        protected override IEnumerator<OutEdge<TVertex>> GetAdjacentVertexes(TVertex vertex)
        {
            List<OutEdge<TVertex>> adjacents = new List<OutEdge<TVertex>>();

            if (vertex is null) throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            if (vertexes.Contains(vertex))
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }

            foreach (Edge<TVertex> edge in edge_list)
            {
                if (edge.Source.Equals(vertex))
                {
                    adjacents.Add(new OutEdge<TVertex>(edge.Destination, edge.Weight));
                }
            }

            return adjacents.GetEnumerator();
        }

        public override List<TVertex> GetAdjacencyList(TVertex vertex)
        {
            List<TVertex> adjacents = new List<TVertex>();

            if (vertex is null) throw new InvalidVertexException($"Vertex {nameof(vertex)} is null");
            if (vertexes.Contains(vertex))
            {
                throw new InvalidVertexException($"Vertex {nameof(vertex)} does not exist in the graph");
            }

            foreach (Edge<TVertex> edge in edge_list)
            {
                if (edge.Source.Equals(vertex))
                {
                    adjacents.Add(edge.Destination);
                }
            }

            return adjacents;
        }
    }
}