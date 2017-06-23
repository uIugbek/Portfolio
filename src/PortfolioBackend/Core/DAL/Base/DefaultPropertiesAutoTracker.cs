using PortfolioBackend.Configurations;
using FastMember;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public class DefaultPropertiesAutoTracker : IDisposable
    {
        #region Instance

        public static DefaultPropertiesAutoTracker Instance
        {
            get
            {
                return AppDependencyResolver.Current.GetService<DefaultPropertiesAutoTracker>();
            }
        }

        #endregion

        #region Ctor

        public DefaultPropertiesAutoTracker()
        {
            _accessors = new Dictionary<Type, Tuple<TypeAccessor, bool, bool, bool>>();
        }

        #endregion

        #region Fields & Properties
        /// <summary>
        /// IDictionary<Type, Tuple<TypeAccessor, CreatedDate, ModifiedDate, OwnerId>>
        /// </summary>
        private IDictionary<Type, Tuple<TypeAccessor, bool, bool, bool>> _accessors;

        #endregion

        #region Methods

        public void Track(object o, EntityState state, DateTime now, object ownerId = null)
        {
            var type = o.GetType();

            if (!_accessors.ContainsKey(type))
            {
                var accessor = TypeAccessor.Create(type);
                var members = accessor.GetMembers();
                _accessors.Add(
                    type,
                    new Tuple<TypeAccessor, bool, bool, bool>(
                        accessor,
                        members.Any(a => a.Name == Constants.CREATED_DATE_PROPERTY),
                        members.Any(a => a.Name == Constants.MODIFIED_DATE_PROPERTY),
                        members.Any(a => a.Name == Constants.OWNERID_PROPERTY)));
            }

            var item = _accessors[type];
            if (item.Item2 && state == EntityState.Added)
                item.Item1[o, Constants.CREATED_DATE_PROPERTY] = now;
            if (item.Item3)
                item.Item1[o, Constants.MODIFIED_DATE_PROPERTY] = now;
            if (item.Item4 && state == EntityState.Added)
                item.Item1[o, Constants.OWNERID_PROPERTY] = ownerId;
        }

        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            if (_accessors != null)
            {
                _accessors.Clear();
                _accessors = null;
            }
        }
        #endregion
    }
}
