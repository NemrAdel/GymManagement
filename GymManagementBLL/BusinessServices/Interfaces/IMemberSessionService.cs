using GymManagementBLL.View_Models.CategoryVM.MemberSessionviewModel;
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
    }
}
