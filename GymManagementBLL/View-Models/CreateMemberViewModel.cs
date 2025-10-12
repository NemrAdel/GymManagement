using GymManagmentDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models
{
    internal class CreateMemberViewModel
    {

        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Name length between 2 and 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = null!;



        [Required(ErrorMessage = "Email is Required")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Email must between 5 and 100")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage ="Phone Is Required")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        [RegularExpression(@"^(010|012|011|015)\d{8}$")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage ="Date is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }


        [Required(ErrorMessage ="Gender Is Required")]
        public Gender Gender { get; set; }



        [Required(ErrorMessage ="Building Number Is Required")]
        [Range(1,9000,ErrorMessage ="Building Number must be between 1 and 9000")]
        public int BuildingNumber { get; set; }




        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength =2, ErrorMessage = "City between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]

        public string City { get; set; } = null!;



        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength =2, ErrorMessage = "Street between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street can only contain letters and spaces")]
        public string Street { get; set; } = null!;



        [Required(ErrorMessage ="HealthRecord Is Required")]
        public HealthRecordView HealthRecord { get; set; } = null!;
    }
}
