using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISessionRepository SessionsRepository { get; }
        IMemberShipRepository MemberShipRepository { get; }
        IGenericRepository<T> GetRepository<T>()where T : BaseEntity, new();

        int SaveChanges();
    }
}
