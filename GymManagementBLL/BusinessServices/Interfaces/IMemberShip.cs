using GymManagementBLL.View_Models.MemberShipVM;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface IMemberShip
    {
        IEnumerable<MemberShipViewModel> GetAllActiveMemberShip();
        bool Create(CreateMemberShipViewModel membership);

        IEnumerable<Member> GetMemberForDropDown();
        IEnumerable<Plan> GetPlanForDropDown();
    }
}
