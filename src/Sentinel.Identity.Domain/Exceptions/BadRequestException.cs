namespace Sentinel.Identity.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message, "BAD_REQUEST") 
    { 
    }

    public BadRequestException(string message, string errorCode) : base(message, errorCode) 
    { 
    }

    public BadRequestException(string message, Exception innerException) : base(message, "BAD_REQUEST", innerException)
    {
    }

    public BadRequestException(string message, string errorCode, Exception innerException) : base(message, errorCode, innerException)
    {
    }
}
