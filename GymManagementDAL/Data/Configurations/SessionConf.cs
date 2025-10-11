using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class SessionConf : IEntityTypeConfiguration<Session>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(s =>
            {
                s.HasCheckConstraint("EndDateConstraint", "EndDate > StartDate");
                s.HasCheckConstraint("CapacityConstraint", "Capacity between 1 and 25");

                builder.HasOne(s=>s.Trainers).WithMany(t => t.Sessions)
                    .HasForeignKey(s => s.TrainerId);
                    
                
            });

        }
    }
}
