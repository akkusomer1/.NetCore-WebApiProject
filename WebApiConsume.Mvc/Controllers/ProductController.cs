using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WebApiConsume.Mvc.Models;

namespace WebApiConsume.Mvc.Controllers
{

    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            if (User.Identity.IsAuthenticated)
            {
                var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token!);
                return client;
            }
            return null;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
       
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var client = CreateClient();
            if (client != null)
            {
                var response = await client.GetAsync("https://localhost:7236/api/Products");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsStringAsync();

                    var responseModel = JsonConvert.DeserializeObject<CustomResponseModel<List<Product>>>(products);

                    return View(responseModel.Data);
                }
                return View();

            }
            return RedirectToAction("SignIn", "Authentication");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVm createVm)
        {
            var client = CreateClient();
            var jsonModel = JsonConvert.SerializeObject(createVm);

            StringContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7236/api/Products", content);

            if (response.IsSuccessStatusCode)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                TempData["errorMessage"] = ($"Bir hata ile karşılaşıldı. Hata kodu:{(int)response.StatusCode}");
                return View(createVm);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"https://localhost:7236/api/Products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customResponseModel = JsonConvert.DeserializeObject<CustomResponseModel<Product>>(content);
                return View(customResponseModel.Data);
            }
            return View();


        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVm vm)
        {
            var client = CreateClient();
            var jsonData = JsonConvert.SerializeObject(vm);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7236/api/Products", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProducts", "Product");
            }
            else
            {
                return View();
            }
        }
    }
}
