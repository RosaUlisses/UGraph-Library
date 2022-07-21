// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.InteropServices;
using GraphLib;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.AdjacencyList;
using GraphLib.EdgeList;
using GraphLib.AdjacencyMatrix;

Graph<int, Directed> directedEdgeList = new AdjacencyMatrix<int, Directed>();
Graph<int, Undirected> undirectedEdgeList = new AdjacencyMatrix<int, Undirected>();

directedEdgeList.AddVertex(10);
directedEdgeList.RemoveVertex(10);