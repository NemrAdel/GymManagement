using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    public class Member: GymUser
    {
        //join date == created at
        public string? Photo { get; set; }

        // Relations
        public HealthRecord HealthRecord { get; set; }


        public ICollection<MemberSessions> MemberSessions { get; set; }

        public ICollection<MemberShip> MemberShips { get; set; }
    }
}
