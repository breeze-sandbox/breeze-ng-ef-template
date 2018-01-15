using Newtonsoft.Json;
using System.Collections.Generic;

namespace Template.Web.Models
{
  public class UserCredentials
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }

  public class AuthUser
  {
    public string ToJson()
    {
      var output = JsonConvert.SerializeObject(this);
      return output;
    }

    public static AuthUser FromJson(string json)
    {
      var authUser = JsonConvert.DeserializeObject<AuthUser>(json);
      return authUser;
    }

    public long UserId { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public bool IsAdmin { get; set; }
    public int SessionMinutes { get; set; }
  }

}