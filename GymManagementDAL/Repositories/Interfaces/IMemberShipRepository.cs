using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemberShipRepository:IGenericRepository<MemberShip>
    {
        IEnumerable<MemberShip> GetAllWithMemberAndPlan();

        MemberShip GetMemberShipByid(int id);
    }
}
