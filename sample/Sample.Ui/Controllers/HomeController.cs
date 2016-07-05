using Microsoft.AspNetCore.Mvc;

namespace Sample.Ui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
