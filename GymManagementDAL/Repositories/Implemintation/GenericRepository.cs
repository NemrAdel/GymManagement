using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implemintation
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        public int Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public T? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T? Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
