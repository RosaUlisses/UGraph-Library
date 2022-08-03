/*
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
    public class UpdateEdgeWeight
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UpdateEdgeDirectedEdgeList()
        {
            Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.UpdateEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedEdgeList.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void UpdateEdgeUndirectedEdgeList()
        {
            Graph<int, Undirected> undirectedEdgeList = new EdgeList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedEdgeList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedEdgeList.UpdateEdge(new Edge<int>(0, i));
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
        public void UpdateEdgeDirectedAdjacencyList()
        {
            Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyList.UpdateEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedAdjacencyList.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void UpdateEdgeUndirectedAdjacencyList()
        {
            Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyList.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyList.UpdateEdge(new Edge<int>(0, i));
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
        public void UpdateEdgeDirectedAdjacencyMatrix()
        {
            Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedAdjacencyMatrix.UpdateEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedAdjacencyMatrix.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void UpdateEdgeUndirectedAdjacencyMatrix()
        {
            Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedAdjacencyMatrix.UpdateEdge(new Edge<int>(0, i));
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
        public void UpdateEdgeDirectedIncidenceMatrix()
        {
            Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>();
            for (int i = 0; i < 10; i++)
            {
                directedIncidenceMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                directedIncidenceMatrix.UpdateEdge(new Edge<int>(0, i));
            }

            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                result = result && directedIncidenceMatrix.AreConnected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void UpdateEdgeUndirectedIncidenceMatrix()
        {
            Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>();
            for (int i = 0; i < 10; i++)
            {
                undirectedIncidenceMatrix.AddVertex(i);
            }

            for (int i = 0; i < 10; i++)
            {
                undirectedIncidenceMatrix.UpdateEdge(new Edge<int>(0, i));
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
*/