using JointureInterfaceMetier;
using Metier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjetPowWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandLineController : ControllerBase
    {
        public object? Post([FromBody] Data saisie)
        {
            CommandLine? theCommand = null;

            theCommand = new CommandLine(saisie.Command);
            Bol.ExecuteData(theCommand);
            if (theCommand.MessageErreur != "") return null;

            var json = new JsonResult(theCommand.LesProduits);
            if (json != null) json.ContentType = "application/json";
            return json;
        }
    }
    public class Data
    {
        public string? Command { get; set; }
    }

}
