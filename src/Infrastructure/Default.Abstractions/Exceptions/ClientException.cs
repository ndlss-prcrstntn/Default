namespace Default.Abstractions.Exceptions;

public class ClientException : Exception
{
    public const string DefaultMessage = "Запрос не может быть обработан. Попробуйте обновить страницу";

    public ClientException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ClientException(string message) : base(message)
    {
    }

    public ClientException(int code, string message) : base(message)
    {
        Code = code;
    }

    public ClientException(string messageTemplate, params object[] args) : base(string.Format(messageTemplate, args))
    {
    }

    public ClientException(int code, string messageTemplate, params object[] args) : this(messageTemplate, args)
    {
        Code = code;
    }

    public int Code { get; set; } = 500;

    public static bool IsMessageAvailableForUser(Exception ex, out string message)
    {
        message = DefaultMessage;

        if (ex is ClientException)

        {
            message = ex.Message;

            return true;
        }

        return false;
    }
}