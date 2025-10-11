using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implemintation
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            
            _dbContext = dbContext;
        }
        public int Add(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlans()
        {
            return _dbContext.Plans.ToList();
        }

        public Plan? GetPlanById(int id)
        {
            return _dbContext.Plans.Find(id);
        }

        public int Remove(int id)
        {
            var plan = GetPlanById(id);
            if (plan == null) return 0;
            _dbContext.Plans.Remove(plan);
            return _dbContext.SaveChanges();
        }

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
