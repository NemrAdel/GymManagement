using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        IEnumerable<Plan> GetAllPlans();
        Plan? GetPlanById(int id);
        int Add(Plan plan);  // return number of affected rows
        int Update(Plan plan);
        int Remove(int id);
    }
}
