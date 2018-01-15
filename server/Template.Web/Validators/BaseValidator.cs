using Breeze.ContextProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Web.Validators
{
  public abstract class BaseValidator<T> : IValidate
  {
    /// <summary>
    /// Validate the entity, adding errors to the errors collection
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="entity"></param>
    public abstract void Validate(IList<EntityError> errors, EntityInfo entityInfo, DbContext dbContext);

    /// <summary>
    /// Return the key value from the entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract object GetKey(T entity);

    /// <summary>
    /// Return a new error for the given entity/property/message
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="propertyName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public EntityError NewError(T entity, string propertyName, string message)
    {
      return new EntityError()
      {
        EntityTypeName = entity.GetType().Name,
        ErrorMessage = message,
        KeyValues = new object[] { GetKey(entity) },
        PropertyName = propertyName
      };
    }

    public void AddError(IList<EntityError> errors, T entity, string propertyName, string message)
    {
      errors.Add(NewError(entity, propertyName, message));
    }
  }

  public interface IValidate {
    void Validate(IList<EntityError> errors, EntityInfo entityInfo, DbContext dbContext);
  }
}
