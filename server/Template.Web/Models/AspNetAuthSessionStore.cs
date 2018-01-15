using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Template.Web.Models
{
    /// <summary>
    /// Copied from https://github.com/tomi85/Microsoft.Owin/blob/master/tests/Katana.Sandbox.WebServer/AspNetAuthSessionStore.cs
    /// </summary>
    public class AspNetAuthSessionStore : IAuthenticationSessionStore
    {
        public AspNetAuthSessionStore()
        {
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            string key = Guid.NewGuid().ToString();
            HttpContext httpContext = HttpContext.Current;
            CheckSessionAvailable(httpContext);
            //httpContext.Session[key + ".Ticket"] = ticket;
            // Serialization fix from https://stackoverflow.com/a/33614059
            var ticketSerializer = new TicketSerializer();
            var ticketBytes = ticketSerializer.Serialize(ticket);
            httpContext.Session[key + ".Ticket"] = ticketBytes;

            return Task.FromResult(key);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            HttpContext httpContext = HttpContext.Current;
            httpContext.Session[key + ".Ticket"] = ticket;
            return Task.FromResult(0);
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            HttpContext httpContext = HttpContext.Current;
            CheckSessionAvailable(httpContext);
            var ticket = httpContext.Session[key + ".Ticket"] as AuthenticationTicket;
            return Task.FromResult(ticket);
        }

        public Task RemoveAsync(string key)
        {
            HttpContext httpContext = HttpContext.Current;
            CheckSessionAvailable(httpContext);
            httpContext.Session.Remove(key + ".Ticket");
            return Task.FromResult(0);
        }

        private static void CheckSessionAvailable(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new InvalidOperationException("Not running on SystemWeb");
            }
            if (httpContext.Session == null)
            {
                throw new InvalidOperationException("Session is not enabled for this request");
            }
        }
    }

    public static class AspNetSessionExtensions
    {
        public static IAppBuilder RequireAspNetSession(this IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                // Depending on the handler the request gets mapped to, session might not be enabled. Force it on.
                HttpContextBase httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
                return next();
            });
            // SetSessionStateBehavior must be called before AcquireState
            //app.UseStageMarker(PipelineStage.MapHandler);
            return app;
        }

        public static IAppBuilder UseAspNetAuthSession(this IAppBuilder app)
        {
            return app.UseAspNetAuthSession(new CookieAuthenticationOptions());
        }

        public static IAppBuilder UseAspNetAuthSession(this IAppBuilder app, CookieAuthenticationOptions options)
        {
            app.RequireAspNetSession();
            options.SessionStore = new AspNetAuthSessionStore();
            app.UseCookieAuthentication(options, PipelineStage.PreHandlerExecute);
            return app;
        }
    }
}