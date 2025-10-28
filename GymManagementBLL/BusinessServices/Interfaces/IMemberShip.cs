using GymManagementBLL.View_Models.MemberShipVM;
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
        bool Create(MemberShipViewModel membership);
    }
}
