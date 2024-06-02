using Microsoft.AspNetCore.Mvc;

namespace WorkTime.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
