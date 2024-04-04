using Microsoft.AspNetCore.Mvc;
using ProjetPowWeb.Models;

namespace ProjetPowWeb.Controllers
{
    public class XefiController : Controller
    {
        public IActionResult Index()
        {
            var html = new ContentResult();
            html.ContentType = "text/html";
            html.Content = @"
                <html>
                    <body>
                        <h1>Accueil Xefi</h1>
                    </body>
                    <button onclick='f();'>Go</button>
                    <script>
                        function f(){ alert('coucou');}
                    </script>
                </html>";
            return html;
        }
        public IActionResult About()
        {
            // Appel au modèle
            var todo = new Todo { Id = 1, Name="Acheter du pain", Fait=false };

            // Appel à View et retour au client
            return View(todo);

        }
        public IActionResult About2()
        {
            var todo = new Todo { Id = 1, Name = "Acheter du pain", Fait = false };
            
            var html = new ContentResult();
            html.ContentType = "text/html";
            html.Content = @"
                        <html>
                            <body>
                                <ul>
                                <li>@Model.Id</li>
                                <li>@Model.Name</li>
                                <li>@Model.Fait</li>
                            </ul>
                            </body>
                        </html>";
            html.Content = html.Content
                .Replace("@Model.Id", todo.Id.ToString())
                .Replace("@Model.Name", todo.Name)
                .Replace("@Model.Fait", todo.Fait.ToString());
            return html;
        }
    }
}
