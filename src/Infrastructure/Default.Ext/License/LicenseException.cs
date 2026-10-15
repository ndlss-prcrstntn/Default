using Default.Abstractions.Exceptions;

namespace Default.Ext.License;

public class LicenseException : ServiceException
{
    public LicenseException(Exception exception, bool fatal) : base(exception, fatal)
    {
    }

    public LicenseException(string message) : base(message)
    {
    }

    public LicenseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}