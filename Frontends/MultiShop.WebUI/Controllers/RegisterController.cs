using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
            if (createRegisterDto.Password == createRegisterDto.ConfirmPassword)
            {
                var client = _httpClientFactory.CreateClient();

                // Custom SSL/TLS validation
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return errors == SslPolicyErrors.None || ValidateCertificate(cert, chain, errors);
                };

                client = new HttpClient(handler);

                var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }

        static bool ValidateCertificate(X509Certificate2 cert, X509Chain chain, SslPolicyErrors errors)
        {
            // Custom certificate validation logic
            // This will bypass the SSL validation, use only for testing purposes
            return true; // or implement your own validation logic
        }
    }
}
