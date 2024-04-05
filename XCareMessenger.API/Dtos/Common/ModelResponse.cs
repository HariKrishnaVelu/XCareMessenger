namespace XCareMessenger.API.Dtos.Common
{
    public class ModelResponse<T> : Response
    {
        public T Model { get; set; }

    }
}
