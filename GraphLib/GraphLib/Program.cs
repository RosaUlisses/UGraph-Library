// See https://aka.ms/new-console-template for more information

using System.Collections;
using UGraph;
using UGraph.Propertys;
using UGraph.EdgeList;

Graph<char, Directed> g = new EdgeList<char, Directed>();

g.AddVertex('a');
g.AddVertex('c');

foreach (var vertex in g)
{
   Console.WriteLine(vertex); 
}



