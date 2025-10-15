using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    public class PlanToUpdateViewModel
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage ="Description Is Required")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="Description Between 5 and 50")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage ="DurationDays Is Required")]
        [Range(1,365,ErrorMessage ="DurationDays must be between 1 and 365")]
        public int DuratonDays { get; set; }
        [Required(ErrorMessage ="Price Is Required")]
        [Range(250,10000,ErrorMessage ="Price must be between 250 and 10000 EGP")]
        public decimal price { get; set; }
    }
}
