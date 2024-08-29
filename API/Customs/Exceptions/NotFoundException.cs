using API.Customs.Exceptions.Base;

namespace API.Customs.Exceptions
{
    public class NotFoundException(string detail) : ApiException("Not Found", detail, StatusCodes.Status404NotFound)
    {
    }
}