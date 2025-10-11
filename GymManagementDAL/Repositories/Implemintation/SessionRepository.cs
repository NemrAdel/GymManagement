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
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _dbContext;


        public SessionRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return _dbContext.Sessions.ToList();
        }

        public Session? GetSessionById(int id)
        {
            return _dbContext.Sessions.Find(id);
        }

        public int Remove(int id)
        {
            var session = GetSessionById(id);
            if (session == null) return 0;
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
