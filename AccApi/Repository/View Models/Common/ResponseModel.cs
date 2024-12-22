namespace AccApi.Repository.View_Models.Common
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
