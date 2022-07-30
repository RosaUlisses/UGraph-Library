using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Interfaces;
using NUnit.Framework;
using UGraph;
using UGraph.Edge;
using UGraph.AdjacencyList;
using UGraph.EdgeList;
using UGraph.AdjacencyMatrix;
using UGraph.IncidenceMatrix;
using UGraph.Propertys;

namespace Tests
{
    public class AddEdgeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddEdgeDirectedEdgeList()
        {
            Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedEdgeList.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void AddEdgeUndirectedEdgeList()
        {
            Graph<int, Undirected> undirectedEdgeList = new EdgeList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedEdgeList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedEdgeList.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && undirectedEdgeList.AreConnected(0, i)
                                && undirectedEdgeList.AreConnected(i, 0);
            }
            
            Assert.True(result);
        }

        [Test]
        public void AddEdgeDirectedAdjacencyList()
        {
            Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyList.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedAdjacencyList.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void AddEdgeUndirectedAdjacencyList()
        {
            Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyList.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && undirectedAdjacencyList.AreConnected(0, i)
                                && undirectedAdjacencyList.AreConnected(i, 0);
            }
            
            Assert.True(result);
        }

        [Test]
        public void AddEdgeDirectedAdjacencyMatrix()
        {
            Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyMatrix.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedAdjacencyMatrix.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void AddEdgeUndirectedAdjacencyMatrix()
        {
            Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyMatrix.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && undirectedAdjacencyMatrix.AreConnected(0, i)
                                && undirectedAdjacencyMatrix.AreConnected(i, 0);
            }
            
            Assert.True(result);
        }

        [Test]
        public void AddEdgeDirectedIncidenceMatrix()
        {
            Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedIncidenceMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedIncidenceMatrix.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedIncidenceMatrix.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void AddEdgeUndirectedIncidenceMatrix()
        {
            Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedIncidenceMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedIncidenceMatrix.AddEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && undirectedIncidenceMatrix.AreConnected(0, i)
                                && undirectedIncidenceMatrix.AreConnected(i, 0);
            }
            
            Assert.True(result);
        }
    }
}