// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.InteropServices;
using GraphLib;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.AdjacencyList;
using GraphLib.EdgeList;
using GraphLib.AdjacencyMatrix;


Graph<int, Directed> g = new AdjacencyList<int, Directed>();

g.AddVertex(10);
g.AddVertex(11);
g.AddVertex(12);

foreach (var vertx in g)
{
    Console.WriteLine(vertx);
}

foreach (var vertx in g)
{
    Console.WriteLine(vertx);
}


