using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib.Vertex;

namespace GraphLib
{
    public class AdjacencyList<TValue, TType, TWeight, TProperty> : Graph<TValue, TType, TWeight, TProperty>
    {
        private Dictionary<Vertex<TValue>, List<Vertex<TValue>>> adjacenyLists;
        public int Count { get; private set; }
        public AdjacencyList()
        {
            adjacenyLists = new Dictionary<Vertex<TValue>, List<Vertex<TValue>>>();
            Count = 0;
        }
        
        public override void AddVertex(Vertex<TValue> vertex)
        {
            adjacenyLists.Add(vertex, new List<Vertex<TValue>>());
        }
        
        
    }
}