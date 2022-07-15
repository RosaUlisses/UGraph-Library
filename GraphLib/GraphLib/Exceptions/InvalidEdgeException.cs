
namespace GraphLib.Exceptions
{
    public class InvalidEdgeException : Exception
    {
        public InvalidEdgeException() : base() { }
        public InvalidEdgeException(string message) : base(message) { }
        public InvalidVertexException(string message, Exception inner) : base(message, inner) { } 
    }    
}
