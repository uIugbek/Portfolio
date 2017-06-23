using System.Linq.Expressions;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioBackend.Configurations;

namespace PortfolioBackend.Core.BLL.Services
{
    public class CultureService : BaseService<DAL.Culture>
    {
        public static UserService Instance
        {
            get
            {
                return AppDependencyResolver.Current.GetService<UserService>();
            }
        }
        public SelectList GetAsSelectList(Expression<Func<DAL.Culture, bool>> predicate = null)
        {
            var query = AllAsQueryable;

            if (predicate != null)
                query = query.Where(predicate);

            return new SelectList(query.OrderBy(a => a.Code), "Id", "Name");
        }
    }
}
