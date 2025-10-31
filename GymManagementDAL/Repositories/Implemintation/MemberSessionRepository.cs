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
    public class MemberSessionRepository : GenericRepository<MemberSessions>, IMemberSessionRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberSessionRepository(GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberSessions> GetAllMemberSessionsWithMemberAndSessionAndCategoryAndTrainer()
        {
            return _dbContext.MemberSessions.Include(m=>m.Members)
                .Include(ms=>ms.Sessions)
                .ThenInclude(s=>s.Category)
                .Include(ms=>ms.Sessions)
                .ThenInclude(s=>s.Trainers)
                .ToList();
        }

        public int GetCountOfBookesSlots(int SessionId)
        {
            return _dbContext.MemberSessions.Count(ms => ms.SessionId == SessionId);
        }

        public IEnumerable<MemberSessions> GetMemberSessionswithMembers()
        {
            return _dbContext.MemberSessions.Include(x => x.Members).ToList();
        }

        public MemberSessions getMemberSessionById(int id)
        {
            var memberSession = _dbContext.MemberSessions.FirstOrDefault(ms => ms.MemberId == id);
            if (memberSession == null)
            {
                Console.WriteLine($"MemberSession not found. {id}");
            }
            _dbContext.SaveChanges();
            return memberSession;
        }
    }
}
