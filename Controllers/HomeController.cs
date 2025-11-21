using Microsoft.AspNetCore.Mvc;

namespace VoeMais.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Aeronave");
        }
    }
}
