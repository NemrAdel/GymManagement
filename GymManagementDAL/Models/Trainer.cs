using GymManagmentDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    internal class Trainer:GymUser
    {
        public Specialties Specialties { get; set; }

        //hire date == created at

        // Relations
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
