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
    public class RemoveEdgeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RemoveEdgeDirectedEdgeList()
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
                result = result && directedEdgeList.AreConected(0, i);
            }

            directedEdgeList.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !directedEdgeList.AreConected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeUndirectedEdgeList()
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
                result = result && undirectedEdgeList.AreConected(0, i)
                                && undirectedEdgeList.AreConected(i, 0);
            }

            undirectedEdgeList.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !undirectedEdgeList.AreConected(0, i)
                                && !undirectedEdgeList.AreConected(i, 0);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeDirectedAdjacencyList()
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
                result = result && directedAdjacencyList.AreConected(0, i);
            }

            directedAdjacencyList.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !directedAdjacencyList.AreConected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeUndirectedAdjacencyList()
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
                result = result && undirectedAdjacencyList.AreConected(0, i)
                                && undirectedAdjacencyList.AreConected(i, 0);
            }

            undirectedAdjacencyList.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !undirectedAdjacencyList.AreConected(0, i)
                                && !undirectedAdjacencyList.AreConected(i, 0);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeDirectedAdjacencyMatrix()
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
                result = result && directedAdjacencyMatrix.AreConected(0, i);
            }

            directedAdjacencyMatrix.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !directedAdjacencyMatrix.AreConected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeUndirectedAdjacencyMatrix()
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
                result = result && undirectedAdjacencyMatrix.AreConected(0, i)
                                && undirectedAdjacencyMatrix.AreConected(i, 0);
            }

            undirectedAdjacencyMatrix.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !undirectedAdjacencyMatrix.AreConected(0, i)
                                && !undirectedAdjacencyMatrix.AreConected(i, 0);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeDirectedIncidenceMatrix()
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
                result = result && directedIncidenceMatrix.AreConected(0, i);
            }

            directedIncidenceMatrix.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !directedIncidenceMatrix.AreConected(0, i);
            }

            Assert.True(result);
        }

        [Test]
        public void RemoveEdgeUndirectedIncidenceMatrix()
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
                result = result && undirectedIncidenceMatrix.AreConected(0, i)
                                && undirectedIncidenceMatrix.AreConected(i, 0);
            }

            undirectedIncidenceMatrix.ClearEdges();

            for (int i = 0; i < 10; i++)
            {
                result = result && !undirectedIncidenceMatrix.AreConected(0, i)
                                && !undirectedIncidenceMatrix.AreConected(i, 0);
            }

            Assert.True(result);
        }
    }
}