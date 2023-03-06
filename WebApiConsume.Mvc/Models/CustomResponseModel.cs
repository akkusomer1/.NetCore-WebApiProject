namespace WebApiConsume.Mvc.Models
{
    public class CustomResponseModel<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
