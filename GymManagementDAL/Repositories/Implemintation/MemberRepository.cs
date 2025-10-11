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
    internal class MemberRepository : IMemberRepository
    {

        private readonly GymDbContext _dbContext  = new GymDbContext();
        public int Add(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _dbContext.Members.ToList();
        }

        public Member? GetMemberById(int id) =>_dbContext.Members.Find(id);
        

        public int Remove(int id)
        {
            var member = GetMemberById(id);
            if (member == null) return 0;
            _dbContext.Members.Remove(member);
            return _dbContext.SaveChanges();
        }

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
