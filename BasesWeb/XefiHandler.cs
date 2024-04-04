using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BasesWeb
{
    public class XefiHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var fichier = context.Server.MapPath(context.Request.FilePath);
            if (File.Exists(fichier))
            {
                var data = File.ReadAllText(fichier);
                context.Response.Write(data);
            }

        }
    }
}