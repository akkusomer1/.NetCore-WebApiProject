using System.Text.Json.Serialization;

namespace WebApiConsume.Mvc.Models
{
    public class SignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public bool RememberMe { get; set; }
    }
}
