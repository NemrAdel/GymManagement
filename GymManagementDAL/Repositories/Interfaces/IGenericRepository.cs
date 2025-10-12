using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<T>where T: BaseEntity, new()
    {
        T? GetById(int id);

        IEnumerable<T> GetAll(Func<T,bool>? condition=null);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
