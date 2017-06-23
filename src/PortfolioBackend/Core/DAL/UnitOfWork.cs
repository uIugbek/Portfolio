using PortfolioBackend.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public interface IUnitOfWork<TContext>
        : IDisposable
        where TContext : DbContext
    {
        TContext DbContext { get; }

        TService GetService<TService>();

        void Commit();
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public UnitOfWork(TContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public TContext DbContext
        {
            get { return _dbContext; }
        }

        public async void Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public TService GetService<TService>()
        {
            return AppDependencyResolver.Current.GetService<TService>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
