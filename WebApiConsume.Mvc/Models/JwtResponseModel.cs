namespace WebApiConsume.Mvc.Models
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
