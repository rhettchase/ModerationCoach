using Microsoft.AspNetCore.Mvc;

namespace ModerationCrudApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Users");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
