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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllWithCategoryAndTrainer()
        {
            return _dbContext.Sessions.Include(s => s.Category)
                .Include(s => s.Trainers).ToList();

        }

        public Session? GetByIdWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions
                .Include(s => s.Category)
                .Include(s => s.Trainers)
                .FirstOrDefault(s => s.Id == sessionId);
        }

        public int GetCountOfBookesSlots(int SessionId)
        {
            return _dbContext.MemberSessions.Count(ms => ms.SessionId == SessionId);
        }
    }
}
