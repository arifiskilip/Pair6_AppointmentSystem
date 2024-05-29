namespace Core.CrossCuttingConcers.Exceptions.Types
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }
    }
}
