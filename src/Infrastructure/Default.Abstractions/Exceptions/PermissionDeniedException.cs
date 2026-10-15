namespace Default.Abstractions.Exceptions;

public class PermissionDeniedException(string message) : ClientException(message)
{
    public PermissionDeniedException()
        : this("Доступ запрещен")
    {
    }
}
