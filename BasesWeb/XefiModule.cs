using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasesWeb
{
    public class XefiModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            context.EndRequest += Context_EndRequest;
        }

        private void Context_EndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            app.Response.Write("Envoi de la demande dans Context_EndRequest\n ");
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
           var app = (HttpApplication)sender;
            app.Response.Write("Reception de la demande dans Context_BeginRequest\n");
        }
    }
}