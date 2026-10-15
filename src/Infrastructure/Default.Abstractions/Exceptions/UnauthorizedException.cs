namespace Default.Abstractions.Exceptions;

public class UnauthorizedException(string message) : ClientException(message)
{
    public UnauthorizedException() : this("Ошибка авторизации")
    {
    }
}
