using GymManagementBLL.View_Models.SessionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionsDetails(int sessionId);

        bool CreateSession(CreateSessionViewModel createSessionViewModel);
    }
}
