using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Template.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Template.Web.Util;
using Template.Data;

namespace Template.Web.Controllers {
  [RoutePrefix("api/Login")]
  public class LoginController : ApiController {

    private TemplateContext GetTemplateContext()
    {
      var connectionString = ConfigUtil.GetConnectionString("NorthwindIB");
      return new TemplateContext(connectionString);
    }

    [HttpPost]
    public AuthUser Login(UserCredentials credentials) {
      LogoutInternal();
      var user = AuthenticateUser(credentials);

      var claims = new List<Claim>();
      claims.Add(new Claim(SecurityUtil.USERID, "" + user.UserId));
      claims.Add(new Claim(SecurityUtil.USERNAME, user.UserName));
      var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

      var ctx = HttpContext.Current.GetOwinContext();
      var authenticationManager = ctx.Authentication;
      authenticationManager.SignIn(id);
      SecurityUtil.SetAuthUser(user);
      return user;
    }

    private AuthUser AuthenticateUser(UserCredentials credentials) {
      // TODO this is fake authentication that just checks if user is in the database.
      if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Password)) {
        throw new AuthenticationException("Invalid UserName or Password");
      }

      var context = GetTemplateContext();
      var user = context.Users.Where(u => u.UserName == credentials.UserName).FirstOrDefault();
      if (user == null) {
        throw new AuthenticationException("Invalid UserName or Password");
      }

      return MakeUser(user);
    }

    [HttpGet]
    public void Logout() {
      LogoutInternal();
      HttpContext.Current.Session.Abandon();
    }

    private void LogoutInternal() {
      var ctx = HttpContext.Current.GetOwinContext();
      var authenticationManager = ctx.Authentication;
      authenticationManager.SignOut();
      HttpContext.Current.Session.Clear();
    }

    [HttpGet, Authorize] 
    public string KeepAlive() { 
      return "ok";
    }

    [HttpGet, Authorize]
    public AuthUser GetLoggedInUser() {
      var userId = SecurityUtil.GetUserId();
      var context = GetTemplateContext();
      var user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
      if (user == null) {
        throw new AuthenticationException("Not logged in");
      }
      var authUser = MakeUser(user);
      SecurityUtil.SetAuthUser(authUser);
      return authUser;
    }

    /// <summary>
    /// Create a user object to be stored in session and sent to client
    /// </summary>
    /// <param name="ulc">User record</param>
    /// <returns></returns>
    private AuthUser MakeUser(User ulc) {
      int timeout = (int)Startup.GetExpireTimeSpan().TotalMinutes;

      AuthUser user;

      user = new AuthUser() {
        IsAdmin = false,
        UserId = ulc.Id,
        UserName = ulc.UserName,
        DisplayName = ulc.FirstName + ' ' + ulc.LastName,
        SessionMinutes = timeout,
      };

      return user;
    }


  }


}
