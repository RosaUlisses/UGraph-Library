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

Graph<char, Directed> g = new AdjacencyList<char, Directed>();

g.AddVertex('a');
g.AddVertex('c');

g.AddEdge(new Edge<char>('a', 'b'));
g.AddEdge(new Edge<char>('a', 'b', 20));

Console.WriteLine("ponto");


