namespace API.Utils
{
    public class PagedList<T> : List<T> 
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            PaginationMetaData = new PaginationMetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public PaginationMetaData PaginationMetaData { get; set; }

        public static PagedList<T> ToPagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}