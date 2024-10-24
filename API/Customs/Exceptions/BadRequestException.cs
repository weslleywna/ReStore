using API.Customs.Exceptions.Base;

namespace API.Customs.Exceptions
{
    public class BadRequestException(string detail) : ApiException("Bad Request", detail, StatusCodes.Status400BadRequest)
    {
    }
}