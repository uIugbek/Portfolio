using PortfolioBackend.Configurations;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.BLL.Services
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> DbSet { get; }
        
        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity,
                    bool directly = false,
                    Type[] notIncludedEntityTypes = null);
    }

    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity[] All { get; }

        IQueryable<TEntity> AllAsQueryable { get; }

        TEntity ByID(object id);
        
        TEntity Create(TEntity entity);
        
        TEntity Update(TEntity entity);
        
        void Delete(object id,
                    bool directly = false,
                    Type[] notIncludedEntityTypes = null);

        void Delete(TEntity entity,
                    bool directly = false,
                    Type[] notIncludedEntityTypes = null);

        bool TryCreate(ref TEntity entity);

        bool TryUpdate(ref TEntity entity);

        bool TryDelete(TEntity entity,
                       bool directly = false,
                       Type[] notIncludedEntityTypes = null);
    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        #region Fields

        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWork<DbContext> _unitOfWork;
        //protected readonly DbContext _dbContext;

        #endregion

        #region Constructor

        public BaseRepository(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.DbContext.Set<TEntity>();
        }

        #endregion

        #region Properties

        public DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        private Type _typeOfTEntity;
        public Type TypeOfTEntity
        {
            get
            {
                if (_typeOfTEntity == null)
                    _typeOfTEntity = typeof(TEntity);
                return _typeOfTEntity;
            }
        }

        private bool? _isDeletedEnable;
        private bool IsDeletedEnable
        {
            get
            {
                if (_isDeletedEnable == null)
                    _isDeletedEnable = EntityProperties.IsDeleted &&
                        TypeOfTEntity.GetProperty(Constants.IS_DELETED_PROPERTY) != null;
                return _isDeletedEnable.GetValueOrDefault(false);
            }
        }
        
        #endregion

        #region Methods

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);

            _unitOfWork.DbContext.Entry(entity).State = EntityState.Added;

            _unitOfWork.Commit();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);

            _unitOfWork.DbContext.Entry(entity).State = EntityState.Modified;

            _unitOfWork.Commit();

            return entity;
        }

        public void Delete(TEntity entity,
                           bool directly = false,
                           Type[] notIncludedEntityTypes = null)
        {
            if (_unitOfWork.DbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            if (directly || !IsDeletedEnable)
            {
                _dbSet.Remove(entity);

                _unitOfWork.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                entity.MarkAsDeleted(
                    typeOfEntity: TypeOfTEntity,
                    notInculedEntityTypes: notIncludedEntityTypes);
            }

            _unitOfWork.Commit();
        }

        #endregion
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        #region Fields

        private readonly IBaseRepository<TEntity> _repository;
        protected readonly IUnitOfWork<DbContext> _unitOfWork;

        #endregion

        #region Constructors

        public BaseService()
        {
            _unitOfWork = new UnitOfWork<DbContext>(new PostgresDbContext());//ApplicationDbContext
            _repository = new BaseRepository<TEntity>(_unitOfWork);
        }

        #endregion

        #region Properties

        public virtual TEntity[] All
        {
            get { return _repository.DbSet.ToArray(); }
        }

        public virtual IQueryable<TEntity> AllAsQueryable
        {
            get { return _repository.DbSet.AsQueryable(); }
        }

        #endregion

        #region Methods

        public virtual IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _repository.DbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.AsQueryable();
        } 

        public virtual TEntity ByID(object id)
        {
            return _repository.DbSet.Find(id);
        }

        public async virtual Task<TEntity> ByIDAsync(object id)
        {
            return await _repository.DbSet.FindAsync(id);
        }

        public virtual TEntity ByID(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _repository.DbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Where(predicate).FirstOrDefault();
        }

        public async virtual Task<TEntity> ByIDAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _repository.DbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual TEntity Create(TEntity entity)
        {
            return _repository.Create(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public virtual void Delete(object id,
                                   bool directly = false,
                                   Type[] notIncludedEntityTypes = null)
        {
            TEntity entityToDelete = ByID(id);

            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity,
                                   bool directly = false,
                                   Type[] notIncludedEntityTypes = null)
        {
            _repository.Delete(entity, directly, notIncludedEntityTypes);
        }

        public virtual bool TryCreate(ref TEntity entity)
        {
            try
            {
                entity = _repository.Create(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool TryUpdate(ref TEntity entity)
        {
            try
            {
                entity = _repository.Update(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public virtual bool TryDelete(TEntity entity,
                                       bool directly = false,
                                       Type[] notIncludedEntityTypes = null)
        {
            try
            {
                _repository.Delete(entity, directly, notIncludedEntityTypes);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
