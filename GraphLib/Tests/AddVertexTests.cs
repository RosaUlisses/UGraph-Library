using NUnit.Framework;
using UGraph;
using UGraph.AdjacencyList;
using UGraph.EdgeList;
using UGraph.AdjacencyMatrix;
using UGraph.IncidenceMatrix;
using UGraph.Propertys;

namespace Tests
{
    public class AddVertexTests 
    {

        int vertex = 10;
        
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void AddVertexEdgeList()
        {
            Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
            Graph<int, Undirected> undirectedEdgeList = new EdgeList<int,Undirected>();

            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.AddVertex(i);
                undirectedEdgeList.AddVertex(i);
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && (directedEdgeList.Contains(i) && undirectedEdgeList.Contains(i));
            }
            Assert.True(result);
        }

        [Test]
        public void AddVertexAdjacencyList()
        {
            Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int,Directed>();
            Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int,Undirected>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyList.AddVertex(i);
                undirectedAdjacencyList.AddVertex(i);
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && (directedAdjacencyList.Contains(i) && undirectedAdjacencyList.Contains(i));
            }
            Assert.True(result);
        }

        [Test]
        public void AddVertexAdjacencyMatrix()
        {
            Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
            Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyMatrix.AddVertex(i);
                undirectedAdjacencyMatrix.AddVertex(i);
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && (directedAdjacencyMatrix.Contains(i) && undirectedAdjacencyMatrix.Contains(i));
            }
            Assert.True(result);
        }
        
         [Test]
         public void AddVertexIncidenceMatrix()
         {
            Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>(); 
            Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>(); 
            for (int i = 0; i < 10; i++)
            {
                directedIncidenceMatrix.AddVertex(i);
                undirectedIncidenceMatrix.AddVertex(i);
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && (directedIncidenceMatrix.Contains(i) && undirectedIncidenceMatrix.Contains(i));
            }
            Assert.True(result);
         }       
    }
}