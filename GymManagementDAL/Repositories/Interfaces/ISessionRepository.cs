using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        IEnumerable<Session> GetAllSessions();
        Session? GetSessionById(int id);
        int Add(Session session);  // return number of affected rows
        int Update(Session session);
        int Remove(int id);
    }
}
