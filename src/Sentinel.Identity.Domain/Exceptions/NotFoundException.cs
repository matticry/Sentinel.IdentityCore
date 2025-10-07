namespace Sentinel.Identity.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message, "NOT_FOUND") 
    { 
    }

    public NotFoundException(string message, string errorCode) : base(message, errorCode) 
    { 
    }

    public NotFoundException(string message, Exception innerException) : base(message, "NOT_FOUND", innerException)
    {
    }

    public NotFoundException(string message, string errorCode, Exception innerException) : base(message, errorCode, innerException)
    {
    }
}
