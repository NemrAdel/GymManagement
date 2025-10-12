using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implemintation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;

        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public IEnumerable<T> GetAll(Func<T, bool>? condition = null)
        {
            if (condition is null)
                return _dbContext.Set<T>().AsNoTracking().ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().Where(condition).ToList();
        }

        public T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }



    }
}
