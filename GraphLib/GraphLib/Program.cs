using System.Collections;
using UGraph;
using UGraph.Propertys;
using UGraph.EdgeList;

Graph<char, Directed> g = new EdgeList<char, Directed>();

g.AddVertex('a');
g.AddVertex('b');
g.AddVertex('c');
g.AddVertex('d');


g.AddEdge(new Edge<char>('a', 'b'));
g.AddEdge(new Edge<char>('a', 'c'));
g.AddEdge(new Edge<char>('c', 'd'));


