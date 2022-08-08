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
Graph<char, Directed> g2 = new IncidenceMatrix<char, Directed>();

g.AddVertex('a');
g.AddVertex('c');
g.AddVertex('e');

g2.AddVertex('a');
g2.AddVertex('c');
g2.AddVertex('e');

g.AddEdge(new Edge<char>('a', 'c'));

Console.WriteLine(g.Equals(g2));

