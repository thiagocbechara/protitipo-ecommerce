namespace PrototipoEcommerce.Domain.Exceptions;

public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string? message) : base(message)
    {
    }
}
