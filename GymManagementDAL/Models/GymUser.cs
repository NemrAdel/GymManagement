using GymManagmentDAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    public  abstract class GymUser: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public Address Address { get; set; }
    }
    [Owned]
     public class Address
    {
        public int Building_no { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
