namespace XCareMessenger.API.Dtos.Common
{
    public class ListResponse<T> : Response
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<T> Model { get; set; }
        public int Count { get; set; }
        public int TotalRecord { get; set; }
    }
}
