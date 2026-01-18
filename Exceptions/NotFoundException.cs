using System.Net;

namespace Products.Api.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message)
        : base(message, (int)HttpStatusCode.NotFound)
    {
    }
}
