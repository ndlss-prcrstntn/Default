namespace Default.Web.Errors;

public class ApiResponse(int statusCode, string message = null)
{
    public int StatusCode { get; set; } = statusCode;

    public string Message { get; set; } = message ?? GetDefaultMessageForStatusCode(statusCode);

    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "You have made a bad request",
            401 => "You are not authorized",
            404 => "Resource was not found",
            500 => "Server error",
            _ => null
        };
    }
}