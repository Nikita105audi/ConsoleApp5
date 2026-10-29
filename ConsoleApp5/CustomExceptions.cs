namespace RunTracker
{
    public class InvalidRunDataException : Exception
    {
        public InvalidRunDataException(string message) : base(message) { }
    }
}