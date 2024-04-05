using JointureInterfaceMetier;
using Metier;
using Microsoft.AspNetCore.Mvc;
using ProjetPowWeb.Models;

namespace ProjetPowWeb.Controllers
{
    public class CommandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CommandModel cmd)
        {
            var commandLine = new CommandLine(cmd.Saisie);
            if (commandLine.MessageErreur != "")
            {
                ViewBag.MessageErreur = commandLine.MessageErreur;
                return View();
            }
            Bol.ExecuteData(commandLine);
            if (commandLine.MessageErreur != "")
            {
                ViewBag.MessageErreur = commandLine.MessageErreur;
                return View();
            }
            return View(commandLine.LesProduits);
        }
    }
}
