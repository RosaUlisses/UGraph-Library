using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib.Vertex;

namespace GraphLib
{
    public abstract class Graph<TVertex, TEdge, TGraphType>
    {
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex value);
        public abstract void AddEdge(TEdge edge);
        public abstract void RemoveEdge(TEdge edge);
    }
}
