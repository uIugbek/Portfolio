using PortfolioBackend.Configurations;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioBackend
{
    public static class EntityExtensions
    {
        public static void MarkAsDeleted(this object entity,
                                          Type typeOfEntity = null,
                                          PropertyInfo isDeletedProperty = null,
                                          Type[] notInculedEntityTypes = null)
        {
            if (entity == null)
                return;

            if (typeOfEntity == null)
                typeOfEntity = entity.GetType();

            if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfEntity.Name))
                return;

            if (isDeletedProperty == null)
                isDeletedProperty = typeOfEntity.GetProperty(Constants.IS_DELETED_PROPERTY);

            if (isDeletedProperty == null)
                return;

            isDeletedProperty.SetValue(entity, true, null);

            #region Check Has Child Entity

            var entityCollectionProps = typeOfEntity.GetProperties()
                .Where(a => a.PropertyType.Name.Contains("ICollection"));
            Type typeOfChildEnt = null;
            PropertyInfo isDeletedPropOfChild = null;

            foreach (var entityCollectionProp in entityCollectionProps)
            {
                var entCollections = entityCollectionProp.GetValue(entity, null) as IEnumerable;
                if (entCollections == null)
                    continue;

                typeOfChildEnt = GetEnumerableElementType(entityCollectionProp.PropertyType);
                isDeletedPropOfChild = typeOfChildEnt.GetProperty(Constants.IS_DELETED_PROPERTY);
                if (notInculedEntityTypes != null && notInculedEntityTypes.Any(a => a.Name == typeOfChildEnt.Name))
                    continue;

                foreach (var ent in entCollections)
                {
                    ent.MarkAsDeleted(typeOfChildEnt, isDeletedPropOfChild, notInculedEntityTypes);
                }
            }

            #endregion
        }

        public static Type GetEnumerableElementType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            var iface = (from i in type.GetInterfaces()
                         where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                         select i).FirstOrDefault();

            if (iface == null)
                throw new ArgumentException("Does not represent an enumerable type.", "type");

            return GetEnumerableElementType(iface);
        }

        public static TEntityModel ConvertToModel<T, TEntityModel>(this T ent)
            where T : class
            where TEntityModel : IModel<T>
        {
            var tEntityModel = Activator.CreateInstance<TEntityModel>();
            tEntityModel.LoadEntity(ent);

            return tEntityModel;
        }
    }
}
