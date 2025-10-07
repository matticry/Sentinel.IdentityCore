namespace Sentinel.Identity.Domain.Exceptions;

public class DomainException : BaseException
{
    public DomainException(string message) : base(message, "DOMAIN_ERROR") 
    { 
    }

    public DomainException(string message, string errorCode) : base(message, errorCode) 
    { 
    }

    public DomainException(string message, Exception innerException) : base(message, "DOMAIN_ERROR", innerException)
    {
    }

    public DomainException(string message, string errorCode, Exception innerException) : base(message, errorCode, innerException)
    {
    }
}
