// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.InteropServices;
using GraphLib;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.AdjacencyList;
using GraphLib.EdgeList;
using GraphLib.AdjacencyMatrix;
using GraphLib.IncidenceMatrix;


Graph<int, Directed> g = new EdgeList<int, Directed>();

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

Console.WriteLine("oi");


