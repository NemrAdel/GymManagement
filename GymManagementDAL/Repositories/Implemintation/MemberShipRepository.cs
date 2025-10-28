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
    public class MemberShipRepository : GenericRepository<MemberShip>, IMemberShipRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberShipRepository(GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberShip> GetAllWithMemberAndPlan()
        {
            return _dbContext.MemberShips.Include(x => x.Plan)
                .Include(x => x.Member).ToList();
        }

        public MemberShip GetMemberShipByid(int id)
        {
            var memebr=_dbContext.MemberShips.FirstOrDefault(x => x.MemberId == id);
            if (memebr is null)
                return null;
            return memebr;

        }
    }
}
