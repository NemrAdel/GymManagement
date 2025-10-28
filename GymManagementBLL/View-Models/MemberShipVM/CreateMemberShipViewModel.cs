using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.MemberShipVM
{
    public class CreateMemberShipViewModel
    {
        [Required(ErrorMessage ="Member Is Required")]
        public string Member { get; set; } = null!;

        [Required(ErrorMessage = "Plan Is Required")]
        public string Plan { get; set; } = null!;
    }
}
