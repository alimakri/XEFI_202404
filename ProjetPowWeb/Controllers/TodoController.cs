//using Microsoft.AspNetCore.Mvc;
//using ProjetPowWeb.Models;

//namespace ProjetPowWeb.Controllers
//{
//    public class TodoController : Controller
//    {
//        private List<Todo> Todos = new List<Todo>
//            {
//                new Todo{Id = 1, Name = "Acheter du pain", Fait = false },
//                new Todo{Id = 2, Name = "Livrer un article", Fait = true },
//                new Todo{Id = 3, Name = "Sortir le chien", Fait = false }
//            };
//        public IActionResult Index()
//        {
//            return View(Todos);
//        }
//        [HttpPost]
//        public IActionResult Index(Todo todoModifie)
//        {

//            var todoOrigin = Todos.FirstOrDefault(x => x.Id == todoModifie.Id);
//            todoOrigin.Name = todoModifie.Name;
//            todoOrigin.Fait = todoModifie.Fait;
//            return View(Todos);
//        }
//        public IActionResult New()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult New(Todo todoAjoute)
//        {
//            Todos.Add(todoAjoute);
//            return RedirectToAction("index");
//        }
//    }
//}
