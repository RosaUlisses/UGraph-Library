using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLib.Vertex;

namespace GraphLib
{
    public class AdjacencyList<TVertex, TEdge, TGraphType> : Graph<TVertex, TEdge, TGraphType>
    {
        
        public override void AddVertex(TVertex vertex)
        {
            throw new NotImplementedException();
        }

        public override void RemoveVertex(TVertex value)
        {
            throw new NotImplementedException();
        }
        public override void AddEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }
    }
}