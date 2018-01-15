using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using Template.Web.Models;

namespace Template.Web.Util
{
  public static class SecurityUtil
  {
    public const string USERID = "USERID";
    public const string USERNAME = "USERNAME";
    public const string AUTHUSER = "AUTHUSER";

    public static string ComputeHash(string text)
    {
      var sha = new SHA1CryptoServiceProvider();
      var byteValue = System.Text.Encoding.UTF8.GetBytes(text);
      var byteHash = sha.ComputeHash(byteValue);
      sha.Clear();
      return Convert.ToBase64String(byteHash);
    }

    public static string ComputeSalt()
    {
      var guid = System.Guid.NewGuid();
      return guid.ToString();
    }

    /// <returns>Current UserLoginControlId</returns>
    public static int GetUserId()
    {
      var userClaim = GetClaim(USERID);
      int id = 0;
      if (userClaim == null || !int.TryParse(userClaim.Value, out id) || id == 0)
      {
        throw new AuthenticationException("Not logged in");
      }
      return id;
    }

    /// <returns>Current User LoginName</returns>
    public static string GetUserName()
    {
      var userClaim = GetClaim(USERNAME);
      if (userClaim == null || string.IsNullOrEmpty(userClaim.Value))
      {
        throw new AuthenticationException("Not logged in");
      }
      return userClaim.Value;
    }

    public static AuthUser GetAuthUser()
    {
      var user = HttpContext.Current.Session[AUTHUSER];
      if (user == null)
      {
        throw new AppException(401, "Not logged id");
      }
      return user as AuthUser;
    }

    public static void SetAuthUser(AuthUser user)
    {
      HttpContext.Current.Session[AUTHUSER] = user;
    }

    private static Claim GetClaim(string claimType)
    {
      var principal = HttpContext.Current.User;
      var identity = (ClaimsIdentity)principal.Identity;
      var claims = identity.Claims;

      var userClaim = claims.SingleOrDefault(x => x.Type == claimType);
      return userClaim;
    }

  }
}