using System.Globalization;

namespace ArconsSystem.Api.Common;

public class AppException : Exception
{
    public AppException() : base() {}

    public AppException(string message) : base(message) { }

    public AppException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public AppException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}