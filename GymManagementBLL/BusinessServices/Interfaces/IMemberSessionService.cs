using GymManagementBLL.View_Models.CategoryVM.MemberSessionviewModel;
using GymManagementBLL.View_Models.MemberSessionviewModel;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface IMemberSessionService
    {
        IEnumerable<MemberSessionViewModel> GetAllMemberSession();
        IEnumerable<OnGoingViewModel> GetOnGoingSessionsBySessionId(int id);

        MemberSessions Attendance(int id);

        IEnumerable<UpComingViewModel> GetMembersWithSessionId(int id);

        IEnumerable<MemberSessions> GetMemberSessionWithMemberAndSession();
        bool Create(CreateMemberSessionViewModel createMember);

        bool Cancel(int id);

        IEnumerable<MemberShip> AllActiveMember();
    }
}
