using Microsoft.AspNetCore.Mvc;

namespace ThucHanh_2.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
