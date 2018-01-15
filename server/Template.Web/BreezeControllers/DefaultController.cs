using Breeze.ContextProvider;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web.Http;
using Template.Data;
using Template.Web.Models;
using Template.Web.Validators;

namespace Template.Web.BreezeControllers
{
  [Authorize]
  [BreezeController]
  public class DefaultController : ApiController
  {
    private TemplateContextProvider _contextProvider;

    public DefaultController()
    {
      var connectionString = ConfigUtil.GetConnectionString("NorthwindIB");
      _contextProvider = new TemplateContextProvider(connectionString);

      _contextProvider.BeforeSaveEntitiesDelegate += BeforeSaveEntities;
      //_contextProvider.AfterSaveEntitiesDelegate += AfterSaveEntities;

    }

    private Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> entities)
    {
      var context = _contextProvider.Context;
      AllValidator.Validate(entities, context);

      return entities;
    }

    [HttpPost]
    [Authorize]
    public SaveResult SaveChanges(JObject saveBundle)
    {
      return _contextProvider.SaveChanges(saveBundle);
    }

    [HttpGet]
    public IQueryable<User> Users()
    {
      return _contextProvider.Context.Users;
    }

  }
}
