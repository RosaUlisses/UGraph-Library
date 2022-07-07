// See https://aka.ms/new-console-template for more information

using System.Collections;
using GraphLib;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.Vertex;
using GraphLib.AdjacencyList;

Graph<int, Edge<int>, Directed> g = new AdjacencyList<int, Edge<int>, Directed, List<OutEdge<int>>, Dictionary<int, List<OutEdge<int>>>>();
g.AddVertex(0);
g.AddVertex(1);
g.AddVertex(2);
g.AddEdge(new Edge<int>(0, 1, 10));
g.AddEdge(new Edge<int>(1, 2, 10));
g.AddEdge(new Edge<int>(2, 1, 10));
g.AddEdge(new Edge<int>(0, 2, 10));

Console.WriteLine("deu");