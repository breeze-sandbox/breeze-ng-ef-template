using Breeze.ContextProvider;
using System.Collections.Generic;
using System.Data.Entity;
using Template.Data;

namespace Template.Web.Validators
{
  public class UserValidator : BaseValidator<User>
  {
    public override object GetKey(User entity)
    {
      return entity.Id;
    }

    public override void Validate(IList<EntityError> errors, EntityInfo entityInfo, DbContext dbContext)
    {
      var entity = (User)entityInfo.Entity;
      var siteContext = (TemplateContext)dbContext;

      if (entityInfo.EntityState != Breeze.ContextProvider.EntityState.Deleted)
      {
        if (string.IsNullOrWhiteSpace(entity.UserName))
        {
          AddError(errors, entity, "UserName", "UserName is required");
        }
      }
    }

  }
}