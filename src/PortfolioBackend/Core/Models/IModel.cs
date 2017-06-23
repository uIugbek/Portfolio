using PortfolioBackend.Configurations;
using PortfolioBackend.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.Models
{
    public interface IModel<TEntity> where TEntity : class
    {
        void Initialize();

        void LoadEntity(TEntity t);

        TEntity UpdateEntity(TEntity t);

        TEntity CreateEntity();

        void LoadViewBag(dynamic viewBag);

        void AfterCreateEntity(TEntity t);

        void AfterUpdateEntity(TEntity t);

        void AfterDeleteEntity(TEntity t);
    }

    public class Model<TEntity> : IModel<TEntity>
        where TEntity : class
    {
        public Model()
        {
        }

        public Model(TEntity ent)
        {
            if (ent != null)
                LoadEntity(ent);
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadEntity(TEntity t)
        {
            if (t == null)
                return;

            Type entityType = typeof(TEntity);
            Type thisType = GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            PropertyInfo temp = null;
            var accessor = FastMember.TypeAccessor.Create(thisType);

            foreach (PropertyInfo item in thisProperties)
            {
                temp = entityProperties.SingleOrDefault(a => a.Name == item.Name);
                if (temp == null)
                    continue;

                #region is IModel

                if (
                    item.PropertyType
                        .GetInterfaces()
                        .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                    )
                {
                    item.PropertyType
                        .GetMethod("LoadEntity")
                        .Invoke
                        (
                            item.GetValue(this, null),
                            new[] { temp.GetValue(t, null) }
                        );

                    continue;
                }

                #endregion

                #region is non generic type

                if (!(
                         item.PropertyType.GetTypeInfo().IsGenericType
                         &&
                         item.PropertyType
                             .GetGenericArguments()[0]
                             .GetInterfaces()
                             .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                     ))
                {
                    item.SetValue(this, temp.GetValue(t, null), null);
                    continue;
                }

                #endregion

                #region is Inumerable IModel

                Type[] args = item.PropertyType.GetGenericArguments();

                Type childEntityModelType = args[0];

                var list = (IEnumerable)temp.GetValue(t, null);

                var values = (IList)item.GetValue(this, null);
                values.Clear();

                foreach (object childEntity in EntityProperties.UseDefault ? list.IsNotDeleted() : list)
                {
                    object childEntityModel = Activator.CreateInstance(childEntityModelType);
                    childEntityModel.GetType().GetMethod("LoadEntity").Invoke(childEntityModel,
                                                                              new object[] { childEntity });

                    values.Add(childEntityModel);
                }

                item.SetValue(this, values, null);

                #endregion
            }
        }

        public virtual TEntity CreateEntity()
        {
            var t = Activator.CreateInstance<TEntity>();

            Type entityType = typeof(TEntity);
            Type thisType = GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            PropertyInfo temp = null;

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                temp = thisProperties.SingleOrDefault(a => a.Name == entityProperty.Name);

                if (entityProperty.Name.ToLower() == "id" || temp == null)
                    continue;

                #region is IModel

                if (
                    temp.PropertyType
                        .GetInterfaces()
                        .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                    )
                {
                    entityProperty.SetValue
                        (
                            t,
                            temp.PropertyType
                                .GetMethod("CreateEntity")
                                .Invoke
                                (
                                    temp.GetValue(this, null),
                                    new object[] { }
                                )
                            ,
                            null
                        );
                    continue;
                }

                #endregion

                #region is common type

                if (!(
                         temp.PropertyType.GetTypeInfo().IsGenericType
                         &&
                         temp.PropertyType
                             .GetGenericArguments()[0]
                             .GetInterfaces()
                             .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                     ))
                {
                    entityProperty.SetValue(t, temp.GetValue(this, null), null);
                    continue;
                }

                #endregion

                #region is IEnumerable of IModel

                Type[] args = temp.PropertyType.GetGenericArguments();

                Type childEntityType = entityProperty.PropertyType.GetGenericArguments()[0];

                object entityCollection = entityProperty.GetValue(t, null);
                MethodInfo entityCollectionAddMethod = entityCollection.GetType().GetMethod("Add");

                var entityModels = (IEnumerable)temp.GetValue(this, null);

                foreach (object entityModel in entityModels)
                {
                    object childEntity = entityModel.GetType().GetMethod("CreateEntity").Invoke(entityModel, null);

                    entityCollectionAddMethod.Invoke(entityCollection, new[] { childEntity });
                }

                #endregion
            }

            return t;
        }

        public virtual TEntity UpdateEntity(TEntity t)
        {
            if (t == null)
                return t;

            Type entityType = typeof(TEntity);
            Type thisType = GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            PropertyInfo[] thisProperties = thisType.GetProperties();
            PropertyInfo temp = null;

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                temp = thisProperties.SingleOrDefault(a => a.Name == entityProperty.Name);

                if (entityProperty.Name.ToLower() == "id" || temp == null)
                    continue;

                #region is IModel

                if (
                    temp.PropertyType
                        .GetInterfaces()
                        .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                    )
                {
                    temp.PropertyType
                        .GetMethod("UpdateEntity")
                        .Invoke
                        (
                            temp.GetValue(this, null),
                            new[] { entityProperty.GetValue(t, null) }
                        );

                    continue;
                }

                #endregion

                #region is common type

                if (!(
                         temp.PropertyType.GetTypeInfo().IsGenericType
                         &&
                         temp.PropertyType
                             .GetGenericArguments()[0]
                             .GetInterfaces()
                             .SingleOrDefault(a => a.Name.StartsWith("IModel")) != null
                     ))
                {
                    entityProperty.SetValue(t, temp.GetValue(this, null), null);
                    continue;
                }

                #endregion

                #region is IEnumerable of IModel

                Type[] args = temp.PropertyType.GetGenericArguments();

                Type childEntityType = entityProperty.PropertyType.GetGenericArguments()[0];

                object entityCollection = entityProperty.GetValue(t, null);
                var entityCollectionAsIEnumerable = EntityProperties.UseDefault ? ((IEnumerable)entityCollection).IsNotDeleted() : (IEnumerable)entityCollection;

                var entityModels = (IEnumerable)temp.GetValue(this, null);
                object id = -1;
                bool contains = false;
                PropertyInfo idPropertyOfEntity = childEntityType.GetProperty("Id");
                PropertyInfo idPropertyOfEntityModel = args[0].GetProperty("Id");
                MethodInfo updateEntityMethod = args[0].GetMethod("UpdateEntity");
                MethodInfo createEntityMethod = args[0].GetMethod("CreateEntity");
                MethodInfo entityCollectionAddMethod = entityCollection.GetType().GetMethod("Add");
                MethodInfo removeEntityMethod = entityProperty.PropertyType.GetMethod("Remove");

                #region Delete entities

                IList needToDelete = new List<object>();

                foreach (object childEntity in entityCollectionAsIEnumerable)
                {
                    id = idPropertyOfEntity.GetValue(childEntity, null);

                    contains = false;
                    foreach (object entityModel in entityModels)
                    {
                        if (idPropertyOfEntityModel.GetValue(entityModel, null).Equals(id))
                        {
                            contains = true;
                            break;
                        }
                    }

                    if (!contains)
                        needToDelete.Add(childEntity);
                }

                needToDelete.Clear();

                #endregion

                #region Update or insert entities

                foreach (object entityModel in entityModels)
                {
                    id = idPropertyOfEntityModel.GetValue(entityModel, null);

                    contains = false;
                    if ((id is int && (id as int?) != 0) || (id is Guid && (id as Guid?) != Guid.Empty))
                    {
                        foreach (object childEntity in entityCollectionAsIEnumerable)
                        {
                            if (idPropertyOfEntity.GetValue(childEntity, null).Equals(id))
                            {
                                // update entity
                                updateEntityMethod.Invoke(entityModel, new[] { childEntity });

                                contains = true;
                                break;
                            }
                        }
                    }
                    // need to add entity
                    if (!contains)
                    {
                        object childEntity = createEntityMethod.Invoke(entityModel, null);
                        entityCollectionAddMethod.Invoke(entityCollection, new[] { childEntity });
                        continue;
                    }
                }

                #endregion

                #endregion
            }

            return t;
        }

        public virtual void LoadViewBag(dynamic viewBag)
        {
        }

        public virtual void AfterCreateEntity(TEntity t)
        {
        }

        public virtual void AfterUpdateEntity(TEntity t)
        {
        }

        public virtual void AfterDeleteEntity(TEntity t)
        {
        }
    }
}
