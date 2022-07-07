// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.InteropServices;
using GraphLib;
using GraphLib.Edge;
using GraphLib.Propertys;
using GraphLib.Vertex;
using GraphLib.AdjacencyList;

Dictionary<int, double> d1 = new Dictionary<int, double>();
Dictionary<int, double> d2 = new Dictionary<int, double>();


for (int i = 0; i < 10; i++)
{
    d1[i] = i + 10;
    d2[i] = i + 10;
}

DateTime b1 = DateTime.Now;
foreach (var kv in d1.ToList())
{
    var copy = kv;
}
DateTime e1 = DateTime.Now;
TimeSpan delta1 = e1.Subtract(b1);

DateTime b2 = DateTime.Now;
var enun = d2.GetEnumerator();
while (enun.MoveNext())
{
    var copy = enun.Current;
}
DateTime e2 = DateTime.Now;
TimeSpan delta2 = e2.Subtract(b2);

Console.WriteLine(delta1.ToString());
Console.WriteLine(delta2.ToString());

