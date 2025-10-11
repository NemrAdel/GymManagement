using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Models
{
    internal class Category: BaseEntity
    {
        public string CategoryName { get; set; }

        // Relations
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
