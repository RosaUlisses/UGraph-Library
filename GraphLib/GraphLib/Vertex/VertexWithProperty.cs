using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Vertex
{
    public class VertexWithProperty<TValue, TProperty> : Vertex<TValue>
    {
        public TProperty Property { get; private set; }

        public VertexWithProperty(TValue value, TProperty property) : base(value)
        {
            Property = property;
        }
        
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }   
}