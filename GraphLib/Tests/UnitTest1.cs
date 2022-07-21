using NUnit.Framework;
using GraphLib;
using GraphLib.Edge;
using GraphLib.AdjacencyList;
using GraphLib.EdgeList;
using GraphLib.AdjacencyMatrix;
using GraphLib.IncidenceMatrix;
using GraphLib.Propertys;

namespace Tests
{
    public class Tests
    {
        Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
        Graph<int, Undirected> undirectedEdgeList = new EdgeList<int,Undirected>();
        Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
        Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
        Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>(); 
        Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>(); 
        Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int,Directed>();
        Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int,Undirected>();

        private int c = 10;
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddEdgeDirectedGraph()
        {
        }
        
        [Test]
        public void AddEdgeUndirectedGraph()
        {
        }
    }
}