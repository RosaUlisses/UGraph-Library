
namespace GraphLib.Exceptions
{
    public class InvalidEdgeException : Exception
    {
        public InvalidEdgeException() : base() { }
        public InvalidEdgeException(string message) : base(message) { }
        public InvalidEdgeException(string message, Exception inner) : base(message, inner) { } 
    }    
}
