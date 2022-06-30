using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib.Vertex;

namespace GraphLib
{
    public abstract class Graph<TValue, TType, TWeight, TProperty>
    {
        public abstract void AddVertex(Vertex<TValue> vertex);
        // public abstract void RemoveVertex(TValue value);
        // public abstract void AddEdge(TValue source, TValue destination);
        // public abstract void RemoveEdge(TValue source, TValue destination);
    }
}
