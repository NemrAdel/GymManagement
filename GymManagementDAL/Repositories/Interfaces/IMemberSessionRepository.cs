using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepository
    {
        IEnumerable<Session> GetAllMemberSessionsWithMemberAndSessionAndCategoryAndTrainer();
        public int GetCountOfBookesSlots(int SessionId);

        public IEnumerable<MemberSessions> GetMemberSessionswithMembers();

        MemberSessions getMemberSessionById(int id);


        IEnumerable<MemberSessions> GetMemberSessionWithMemberAndSession();



    }
}
