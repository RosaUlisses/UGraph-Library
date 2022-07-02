using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex;
using GraphLib.Edge;

namespace GraphLib
{
    public class AdjacencyList<TVertex, TEdge, TWheight, TGraphType> : 
        Graph<TVertex, TEdge, TWheight,TGraphType> where TEdge : IEdge<TVertex, TGraphType>
    {
        public AdjacencyList()
        {
        }
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