// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.InteropServices;
using UGraph;
using UGraph.Edge;
using UGraph.Propertys;
using UGraph.AdjacencyList;
using UGraph.EdgeList;
using UGraph.AdjacencyMatrix;
using UGraph.IncidenceMatrix;

Graph<char, Directed> g = new IncidenceMatrix<char, Directed>();

g.AddVertex('a');
g.AddVertex('c');
g.AddVertex('e');
g.AddVertex('y');
g.AddVertex('z');
g.AddVertex('u');

g.AddEdge(new Edge<char>('a', 'c', 1));
g.AddEdge(new Edge<char>('a', 'e', 2));
g.AddEdge(new Edge<char>('e', 'z', 3));
g.AddEdge(new Edge<char>('e', 'y', 4));
g.AddEdge(new Edge<char>('z', 'u', 3));
g.AddEdge(new Edge<char>('c', 'z', 5));

var p = g.Dijkstra('a', 'z');

Console.WriteLine("oi");

