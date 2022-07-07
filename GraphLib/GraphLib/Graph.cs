﻿using System;
using System.Collections.Generic;
using System.Linq;
using GraphLib.Vertex;
using GraphLib.Edge;
using GraphLib.Propertys;

namespace GraphLib
{
    public abstract class Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
        where TEdge : IEdge<TVertex>
    {
        protected const int EMPTY_EDGE = 0; 
        public abstract void AddVertex(TVertex vertex);
        public abstract void RemoveVertex(TVertex vertex);
        public abstract void AddEdge(TEdge edge);
        public abstract void RemoveEdge(TEdge edge);
        // public abstract IEnumerator<OutEdge<TVertex>> GetNeihgbours(TVertex vertex);
    }
}
