namespace Default.Abstractions.Exceptions;

public class ServiceException : Exception
{
    public bool Fatal { get; }

    public ServiceException(Exception exception, bool fatal) : base(exception.Message, exception) => Fatal = fatal;

    public ServiceException(string message) : base(message) { }

    public ServiceException(string message, Exception innerException) : base(message, innerException) { }
}