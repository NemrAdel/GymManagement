using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    public class HealthRecordView
    {
        [Required(ErrorMessage ="Height Is Required")]
        [Range(0.1,300,ErrorMessage ="Height must be between 0.1 cm and 300 cm")]
        public decimal Height { get; set; }



        [Required(ErrorMessage = "Weight Is Required")]
        [Range(1, 350, ErrorMessage = "Weight must be between 0.1 kg and 350 kg")]
        public decimal Weight { get; set; }



        [Required(ErrorMessage = "BloodType Is Required")]
        [StringLength(3,ErrorMessage ="BloodType is max 3")]
        public string BloodType { get; set; } = null!;


        public string? Note { get; set; }
    }
}
