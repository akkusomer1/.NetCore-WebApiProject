using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApiConsume.Mvc.Models;

namespace WebApiConsume.Mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient client;
        public AuthenticationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            client = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {

            return View();
        }




        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        //Authentication/SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var jsonData = JsonSerializer.Serialize(signInModel);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7236/api/User/SignInByCreateToken", content);


            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var tokenModel = JsonSerializer.Deserialize<CustomResponseModel<JwtResponseModel>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel!.Data.Token);

                if (token !=null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.AddRange(token.Claims);
                    claims.Add(new Claim("accessToken", tokenModel!.Data.Token));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    AuthenticationProperties authenticationprop = new AuthenticationProperties()
                    {
                        ExpiresUtc = tokenModel.Data.Expires,
                        IsPersistent = signInModel.RememberMe,
                        AllowRefresh=false
                    };


                   await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,claimsPrincipal, authenticationprop);

                    return RedirectToAction("Index", "Authentication");
                }
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre yanlıştır.");
                return View();
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre yanlıştır.");
            return View();

        }



        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
