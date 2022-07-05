using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex;
using GraphLib.Edge;

namespace GraphLib
{
    public abstract class Graph<TVertex, TEdge, TGraphType>
        where TEdge : IEdge<TVertex>
    {
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex vertex);
        public abstract void AddEdge(TEdge edge);
        public abstract void RemoveEdge(TEdge edge);
    }
}
