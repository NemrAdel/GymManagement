using GymManagementDAL.Repositories.Implemintation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories=new ();
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            // T ==Member
            var entityType = typeof(T); // Member
            if (_repositories.ContainsKey(entityType))
            {
                return (IGenericRepository<T>)_repositories[entityType];
            }

            if (_repositories.TryGetValue(entityType, out var repository))
                return (IGenericRepository<T>)repository;

            var newRepository = 
                new GenericRepository<T>(_dbContext);
            _repositories[entityType] = newRepository;

            return newRepository;
        }

        public int saveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
