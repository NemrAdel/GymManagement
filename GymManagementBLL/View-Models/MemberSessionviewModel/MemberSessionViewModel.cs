using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.CategoryVM.MemberSessionviewModel
{
    public class MemberSessionViewModel
    {
        public int SessionId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string TrainerName { get; set; }= null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DateDisplay => $"{StartDate:MMM dd,yyyy}";

        public TimeSpan Duration => EndDate - StartDate;
        public string TimeRangeDisplay => $"{StartDate:hh:mm tt}-{EndDate:hh:mm tt}";

        public int AvalibaleSlots { get; set; }
        public int Capacity { get; set; }
        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now) return "Upcoming";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now) return "OnGoing";
                else return "Completed";

            }
        }

    }
}
