using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using Template.Web.Models;

[assembly: OwinStartup(typeof(Template.Web.Startup))]
namespace Template.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.RequireAspNetSession();
            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = GetExpireTimeSpan(),
                SlidingExpiration = true
            };
            app.UseCookieAuthentication(cookieOptions);

        }

        public static TimeSpan GetExpireTimeSpan()
        {
            int hours = ConfigUtil.GetAppSetting("SessionTimeoutHours", 0);
            int minutes = ConfigUtil.GetAppSetting("SessionTimeoutMinutes", 30);
            return new TimeSpan(hours, minutes, 0);
        }

    }
}