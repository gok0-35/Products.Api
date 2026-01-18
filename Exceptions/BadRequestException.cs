using System.Net;

namespace Products.Api.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message)
        : base(message, (int)HttpStatusCode.BadRequest)
    {
    }
}
