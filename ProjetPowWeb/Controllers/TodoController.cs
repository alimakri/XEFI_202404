using Microsoft.AspNetCore.Mvc;

namespace ProjetPowWeb.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
