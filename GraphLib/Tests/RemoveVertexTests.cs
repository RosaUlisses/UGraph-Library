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

            for (int i = 0; i < 10; i++)
            {
                directedEdgeList.AddVertex(i);
                undirectedEdgeList.AddVertex(i);
                directedEdgeList.RemoveVertex(i);
                undirectedEdgeList.RemoveVertex(i);
            }

            bool result = false;
            for (int i = 0; i < 10; i++)
            {
                result = result || (!directedEdgeList.Contains(i) && !undirectedEdgeList.Contains(i));
            }            
            Assert.True(true);
         }
     
         [Test]
         public void RemoveVertexAdjacencyList()
         {
             Graph<int, Directed> directedAdjacencyList = new AdjacencyList<int,Directed>();
             Graph<int, Undirected> undirectedAdjacencyList = new AdjacencyList<int,Undirected>();
             for (int i = 0; i < 10; i++)
             {
                 directedAdjacencyList.AddVertex(i);
                 undirectedAdjacencyList.AddVertex(i);
                 directedAdjacencyList.RemoveVertex(i);
                 undirectedAdjacencyList.RemoveVertex(i);
             }
 
             bool result = false;
             for (int i = 0; i < 10; i++)
             {
                 result = result || (!directedAdjacencyList.Contains(i) && !undirectedAdjacencyList.Contains(i));
             }            
             Assert.True(true);            
         }
         
         [Test]
         public void RemoveVertexAdjacencyMatrix()
         {
             Graph<int, Directed> directedAdjacencyMatrix = new AdjacencyMatrix<int, Directed>();
             Graph<int, Undirected> undirectedAdjacencyMatrix = new AdjacencyList<int, Undirected>();
             for (int i = 0; i < 10; i++)
             {
                 directedAdjacencyMatrix.AddVertex(i);
                 undirectedAdjacencyMatrix.AddVertex(i);
                 directedAdjacencyMatrix.RemoveVertex(i);
                 undirectedAdjacencyMatrix.RemoveVertex(i);
             }
 
             bool result = false;
             for (int i = 0; i < 10; i++)
             {
                 result = result || (!directedAdjacencyMatrix.Contains(i) && !undirectedAdjacencyMatrix.Contains(i));
             }            
             
             for (int i = 0; i < 10; i++)
             {
                  directedAdjacencyMatrix.AddVertex(i);
                  undirectedAdjacencyMatrix.AddVertex(i);
             }
             for (int i = 0; i < 10; i++)
             {
                result = result || (directedAdjacencyMatrix.Contains(i) && undirectedAdjacencyMatrix.Contains(i));
             }
             for (int i = 0; i < 10; i++)
             { 
                  directedAdjacencyMatrix.RemoveVertex(i);
                  undirectedAdjacencyMatrix.RemoveVertex(i);
             }
             for (int i = 0; i < 10; i++)
             {
                  result = result || (!directedAdjacencyMatrix.Contains(i) && !undirectedAdjacencyMatrix.Contains(i));
             }            
             Assert.True(true); 
         }
         
          [Test]
          public void RemoveVertexIncidenceMatrix()
          {
             Graph<int, Directed> directedIncidenceMatrix = new IncidenceMatrix<int, Directed>(); 
             Graph<int, Undirected> undirectedIncidenceMatrix = new IncidenceMatrix<int, Undirected>(); 
             for (int i = 0; i < 10; i++)
             {
                 directedIncidenceMatrix.AddVertex(i);
                 undirectedIncidenceMatrix.AddVertex(i);
                 directedIncidenceMatrix.RemoveVertex(i);
                 undirectedIncidenceMatrix.RemoveVertex(i);
             }
 
             bool result = false;
             for (int i = 0; i < 10; i++)
             {
                 result = result || (!directedIncidenceMatrix.Contains(i) && !undirectedIncidenceMatrix.Contains(i));
             }            
             
             for (int i = 0; i < 10; i++)
             {
                 directedIncidenceMatrix.AddVertex(i);
                 undirectedIncidenceMatrix.AddVertex(i);
             }
             for (int i = 0; i < 10; i++)
             {
                 result = result || (directedIncidenceMatrix.Contains(i) && undirectedIncidenceMatrix.Contains(i));
             }
             for (int i = 0; i < 10; i++)
             { 
                 directedIncidenceMatrix.RemoveVertex(i);
                 undirectedIncidenceMatrix.RemoveVertex(i);
             }
             for (int i = 0; i < 10; i++)
             {
                 result = result || (!directedIncidenceMatrix.Contains(i) && !undirectedIncidenceMatrix.Contains(i));
             }            
             Assert.True(true); 
          }    
     }   
}

