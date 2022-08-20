namespace UGraph.Exceptions
{
    public class InvalidGraphException : Exception
    {
        public InvalidGraphException() : base() { }
        public InvalidGraphException(string message) : base(message) { }
        public InvalidGraphException(string message, Exception inner) : base(message, inner) { } 
    }    
}