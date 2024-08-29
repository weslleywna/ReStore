namespace API.Customs.Exceptions.Base
{
    public class ApiException(string title, string detail, int status) : Exception(detail)
    {
        public string Title { get; } = title;
        public string Detail { get; } = detail;
        public int Status { get; } = status;
    }
}
