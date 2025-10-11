using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    internal class MemberSessions: BaseEntity
    {
        public bool IsIttended { get; set; }

        // Relations

        public int MemberId { get; set; }

        public Member Members { get; set; }

        public int SessionId { get; set; }
        public Session Sessions { get; set; }
    }
}
