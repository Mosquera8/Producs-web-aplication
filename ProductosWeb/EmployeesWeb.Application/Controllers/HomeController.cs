
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductsWeb.Application.Config;
using ProductsWeb.Application.Models;
using ProductsWeb.Services;
using ProductsWeb.Services.Entities;
using System.Diagnostics;


namespace ProductsWeb.Application.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly LoginService loginService;
        private readonly ApiConfiguration _apiConfiguration;

        public HomeController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;


            loginService = new LoginService(_apiConfiguration.ApiLoginUrl);
        }

        public IActionResult Index()
        {
            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged");
            ViewData["Categorie"] = HttpContext.Session.GetString("Categorie");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Llamar a la API para validar el Login
#pragma warning disable CS8602
#pragma warning disable CS8604
                    if (await IsValidUser(login.Email, login.Password))
                    {
                        return RedirectToAction(nameof(Index));
                    }
#pragma warning disable CS8602
#pragma warning disable CS8604
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("IsUserLogged", "false");
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsValidUser(string email, string password)
        {
            LoginDto loginDto = new LoginDto { Email = email, Password = password };

            AuthorizationDto authorization = await loginService.ValidUser(loginDto);
            if (authorization != null)
            {
                HttpContext.Session.SetString("access_token", authorization.AccessToken);
                return true;
            }
            return false;
        }
    }
}