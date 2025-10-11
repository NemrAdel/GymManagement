using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IGenericRepository<T>where T: BaseEntity, new()
    {
        T? GetById(int id);

        IEnumerable<T> GetAll(int id);

        int Add(T entity);
        int Update(T entity);
        int? Delete(T entity);

    }
}
