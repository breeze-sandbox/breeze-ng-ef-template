using Breeze.ContextProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Template.Data;

namespace Template.Web.Validators
{
  public class AllValidator
  {
    public static void Validate(Dictionary<Type, List<EntityInfo>> entities, DbContext dbContext)
    {
      if (entities == null) return;
      var errors = new List<EntityError>();

      foreach (var type in entities.Keys)
      {
        var validator = GetValidator(type);
        if (validator != null)
        {
          var list = entities[type];
          foreach (var entityInfo in list)
          {
            validator.Validate(errors, entityInfo, dbContext);
          }
        }
      }
      if (errors.Count > 0)
      {
        throw new EntityErrorsException(errors);
      }
    }

    public static IValidate GetValidator(Type type)
    {
      if (type == typeof(User)) return new UserValidator();

      return null;
    }
  }
}