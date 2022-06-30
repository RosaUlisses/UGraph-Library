using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib.Vertex;

namespace GraphLib
{
    public interface IGraph<TValue, TType, TWeight, TProperty>
    {
        public void AddVertex();
        // public void RemoveVertex(TValue value);
        public void AddEdge();
    }
}