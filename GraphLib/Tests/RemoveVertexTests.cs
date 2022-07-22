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
     public class RemoveVertexTests
     { 
     
         int vertex = 10;
         
         [SetUp]
         public void Setup()
         {
         }
         
         [Test]
         public void RemoveVertexEdgeList()
         {
             Graph<int, Directed> directedEdgeList = new EdgeList<int, Directed>();
             Graph<int, Undirected> undirectedEdgeList = new EdgeList<int,Undirected>();
             directedEdgeList.AddVertex(vertex);
             undirectedEdgeList.AddVertex(vertex);
             directedEdgeList.RemoveVertex(vertex);
             undirectedEdgeList.RemoveVertex(vertex);
             Assert.False(directedEdgeList.Contains(vertex) || undirectedEdgeList.Contains(vertex));
         }
     
         [Test]
         public void RemoveVertexAdjacencyList()
         {
             Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int,Directed>();
             Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int,Undirected>();
             directedAdjacencyList.AddVertex(vertex);
             undirectedAdjacencyList.AddVertex(vertex);
             directedAdjacencyList.RemoveVertex(vertex);
             undirectedAdjacencyList.RemoveVertex(vertex);
             Assert.False(directedAdjacencyList.Contains(vertex) || undirectedAdjacencyList.Contains(vertex));
         }
         
         [Test]
         public void RemoveVertexAdjacencyMatrix()
         {
             Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
             Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
             directedAdjacencyMatrix.AddVertex(vertex);
             undirectedAdjacencyMatrix.AddVertex(vertex);  
             directedAdjacencyMatrix.RemoveVertex(vertex);
             undirectedAdjacencyMatrix.RemoveVertex(vertex);
             Assert.False(directedAdjacencyMatrix.Contains(vertex) || undirectedAdjacencyMatrix.Contains(vertex));
         }
         
          [Test]
          public void RemoveVertexIncidenceMatrix()
          {
             Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>(); 
             Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>(); 
              directedIncidenceMatrix.AddVertex(vertex);
              undirectedIncidenceMatrix.AddVertex(vertex);  
              directedIncidenceMatrix.RemoveVertex(vertex);
              undirectedIncidenceMatrix.RemoveVertex(vertex);
              Assert.False(directedIncidenceMatrix.Contains(vertex) || undirectedIncidenceMatrix.Contains(vertex));
          }    
     }   
}

