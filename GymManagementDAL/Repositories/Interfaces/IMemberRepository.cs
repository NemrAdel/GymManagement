using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        IEnumerable<Member> GetAllMembers();
        Member?GetMemberById(int id);
        
        int Add(Member member);  // return number of affected rows
        int Update(Member member);
        int Remove(int id);

    }
}
