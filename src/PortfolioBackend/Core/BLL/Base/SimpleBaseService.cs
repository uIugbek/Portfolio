using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.BLL.Services
{
    public class SimpleBaseService<TEntity> : BaseService<TEntity>
        where TEntity : class
    {
        private static Expression<Func<TEntity, bool>> _checkForIsNotDeleted;
        private static Expression<Func<TEntity, bool>> CheckForIsNotDeleted
        {
            get
            {
                if (_checkForIsNotDeleted == null)
                {
                    var ent = Expression.Parameter(typeof(TEntity), "ent");
                    var prop = Expression.Property(ent, Configurations.Constants.IS_DELETED_PROPERTY);
                    var value = Expression.Constant(false);

                    _checkForIsNotDeleted = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(prop, value), ent);
                }

                return _checkForIsNotDeleted;
            }
        }

        #region Overriden properties

        public IQueryable<TEntity> BaseAllAsQueryable
        {
            get
            {
                return base.AllAsQueryable;
            }
        }


        public override TEntity[] All
        {
            get
            {
                return AllAsQueryable.ToArray();
            }
        }

        public override IQueryable<TEntity> AllAsQueryable
        {
            get
            {
                return base.AllAsQueryable.Where(CheckForIsNotDeleted);
            }
        }

        #endregion

        #region Overriden methods

        public override TEntity ByID(object id)
        {
            var ent = base.ByID(id);
            if (ent != null && CheckForIsNotDeleted.Compile().Invoke(ent))
                return ent;
            return null;
        }

        #endregion
    }
}
