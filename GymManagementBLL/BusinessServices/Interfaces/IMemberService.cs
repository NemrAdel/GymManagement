using GymManagementBLL.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModel? GetMemberDetails(int memberid);

        HealthRecordView? GetMemberHealthDetails(int memberid);

        MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberid);

        bool UpdateMember(int memberId,MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int memberId);
    }
}
