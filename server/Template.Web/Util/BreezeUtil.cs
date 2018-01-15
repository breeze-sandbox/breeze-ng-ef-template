using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Web.Util;

namespace Template.Web.Models
{
    public class BreezeUtil
    {
        public static Dictionary<Type, List<EntityInfo>> BeforeSaveEntitiesHandler(Dictionary<Type, List<EntityInfo>> entities)
        {
            var userName = SecurityUtil.GetUserName();
            var entityErrors = new List<EFEntityError>();
            foreach (var type in entities.Keys)
            {
                CheckCanUpdate(type);

                foreach (var entityInfo in entities[type])
                {
                    // TODO check site permissions on save
                    if (entityInfo.EntityState == Breeze.ContextProvider.EntityState.Added)
                    {
                        //if (!_permissionsRepository.UserCanAddEntity(entities, entityInfo, userId))
                        //    entityErrors.Add(new EFEntityError(entityInfo, "Unauthorized", "User does not have permission to add this entity.", ""));
                    }
                    if (entityInfo.EntityState == Breeze.ContextProvider.EntityState.Modified)
                    {
                        //if (!_permissionsRepository.UserCanEditEntity(entityInfo, userId))
                        //    entityErrors.Add(new EFEntityError(entityInfo, "Unauthorized", "User does not have permission to edit this entity.", ""));
                    }
                    if (entityInfo.EntityState == Breeze.ContextProvider.EntityState.Deleted)
                    {
                        //if (!_permissionsRepository.UserCanDeleteEntity(entityInfo, userId))
                        //    entityErrors.Add(new EFEntityError(entityInfo, "Unauthorized", "User does not have permission to delete this entity.", ""));
                    }

                    SetAuditFields(entityInfo, userName);
                }
            }
            if (entityErrors.Count > 0) throw new EntityErrorsException(entityErrors);
            return entities;
        }

        private static void SetAuditFields(EntityInfo entityInfo, string userName)
        {
            //var entity = entityInfo.Entity as AuditEntityBase; TODO add base class
            //if (entity == null) return;
            var entity = entityInfo.Entity;

            if (entityInfo.EntityState == EntityState.Deleted)
            {
                try
                {
                    ((dynamic)entity).Status = 3;
                    entityInfo.EntityState = EntityState.Modified;
                }
                catch { }
            }

            if (entityInfo.EntityState == EntityState.Added || entityInfo.EntityState == EntityState.Modified)
            {
                try
                {
                    //entityInfo.OriginalValuesMap["UpdatingUserName"] = entity.UpdatingUserName;
                    //entityInfo.OriginalValuesMap["UpdateDate"] = entity.UpdateDate;
                    // TODO add base class or interface; remove dynamic
                    ((dynamic)entity).UpdatingUserName = userName;
                    ((dynamic)entity).UpdateDate = DateTime.Now;
                    entityInfo.ForceUpdate = true;
                }
                catch { }
            }
        }

        /// <summary>
        /// Collect int fields from entities of the given entitytype using the given function
        /// </summary>
        public static List<int> GetIds(Dictionary<Type, List<EntityInfo>> entities, Type type, Func<EntityInfo, int> getIdFn)
        {
          var ids = new List<int>();
          var entityInfos = entities.GetValueOrNull(type);
          if (entityInfos != null) ids.AddRange(entityInfos.Select(getIdFn));
          return ids;
        }


    /// <summary>
    /// Check if user can update the given entity type, based upon their features.
    /// Throws exception if user is not allowed to update
    /// </summary>
    /// <param name="entityType"></param>
    public static void CheckCanUpdate(Type entityType)
    {
      //FeatureEnum feature;
      //if (!TypeFeatureMap.Map.TryGetValue(entityType, out feature) || !SecurityUtil.HasFeature((int)feature))
      //{
      //  throw new AppException(403, "User lacks feature required to update this item");
      //}
    }
  }
}