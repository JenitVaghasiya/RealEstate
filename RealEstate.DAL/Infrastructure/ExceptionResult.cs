namespace RealEstate
{
    public class ExceptionResult
    {
        public string Message { get; private set; }

        public string StackTrace { get; private set; }

        public ExceptionResult(string message, string stackTrace)
        {
            Message = message;
            StackTrace = stackTrace;
        }
    }
}
