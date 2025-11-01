using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.MemberShipVM
{
    public class MemberShipViewModel
    {
        public int MemberId { get; set; }
        public string MName { get; set; } = null!;
        public string PName { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
    }
}
