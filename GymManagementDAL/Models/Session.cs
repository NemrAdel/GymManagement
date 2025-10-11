using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    internal class Session:BaseEntity
    {
        public int Capacity { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relations

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainers { get; set; }

        public List<MemberSessions> MemberSessions { get; set; }
    }
}
