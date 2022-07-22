/*
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
    public class AddEdgeTests 
    {

        int vertex = 10;
        
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void AddEdgeEdgeList()
        {
            Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
            Graph<int, Undirected> undirectedEdgeList = new EdgeList<int,Undirected>();
            directedEdgeList.AddVertex(vertex);
            undirectedEdgeList.AddVertex(vertex);
            Assert.True(directedEdgeList.Contains(vertex) && undirectedEdgeList.Contains(vertex));
        }

        [Test]
        public void AddEdgeAdjacencyList()
        {
            Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int,Directed>();
            Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int,Undirected>();
            directedAdjacencyList.AddVertex(vertex);
            undirectedAdjacencyList.AddVertex(vertex);
            Assert.True(directedAdjacencyList.Contains(vertex) && undirectedAdjacencyList.Contains(vertex));
        }

        [Test]
        public void AddEdgeAdjacencyMatrix()
        {
            Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
            Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
            directedAdjacencyMatrix.AddVertex(vertex);
            undirectedAdjacencyMatrix.AddVertex(vertex);
            Assert.True(directedAdjacencyMatrix.Contains(vertex) && undirectedAdjacencyMatrix.Contains(vertex));
        }
        
         [Test]
         public void AddEdgeIncidenceMatrix()
         {
            Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>(); 
            Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>(); 
             directedIncidenceMatrix.AddVertex(vertex);
             undirectedIncidenceMatrix.AddVertex(vertex);
             Assert.True(directedIncidenceMatrix.Contains(vertex) && undirectedIncidenceMatrix.Contains(vertex));
         }       
    }
}
*/