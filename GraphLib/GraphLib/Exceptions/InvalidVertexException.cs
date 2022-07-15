
namespace GraphLib.Exceptions
{
    public class InvalidVertexException : Exception
    {
        public InvalidVertexException() : base() { }
        public InvalidVertexException(string message) : base(message) { }
        public InvalidVertexException(string message, Exception inner) : base(message, inner) { } 
    }    
}
