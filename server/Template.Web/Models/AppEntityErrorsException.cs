using Breeze.ContextProvider;
using System.Collections.Generic;

namespace Template.Web.Models
{
  public class AppEntityErrorsException : EntityErrorsException
  {
    public AppEntityErrorsException(IEnumerable<EntityError> entityErrors) : base(entityErrors) { }
    public AppEntityErrorsException(string message, IEnumerable<EntityError> entityErrors) : base(message, entityErrors) { }

    public AppEntityErrorsException(EntityInfo entityInfo, object key, string propertyName, string message) : base(new List<EntityError>())
    {
      // TODO add this constructor to breeze
      var ee = new EntityError()
      {
        EntityTypeName = entityInfo.Entity.GetType().Name,
        ErrorMessage = message,
        KeyValues = new object[] { key },
        PropertyName = propertyName
      };
      this.EntityErrors.Add(ee);
    }

  }
}