using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    internal class HealthRecord:BaseEntity 
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }
        public string? Note { get; set; }
        // last update == updated at



        // Relations

    }
}
