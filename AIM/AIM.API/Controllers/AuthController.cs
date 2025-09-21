using Microsoft.AspNetCore.Mvc;

namespace AIM.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
