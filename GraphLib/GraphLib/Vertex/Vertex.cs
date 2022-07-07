using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.Vertex
{
    public class Vertex<T> : IComparable<Vertex<T>>
    where T : IComparable<T>
    {
        public T Value { get; }

        public Vertex(T value)
        {
            // TODO -> levantar execao caso value for null
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Vertex<T> vertex = (Vertex<T>)obj;
            return Value.Equals(vertex.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(Vertex<T>? other)
        {
            return Value.CompareTo(other.Value);
        }
    }   
}

