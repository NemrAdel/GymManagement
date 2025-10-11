using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class PlanConf : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(50)");
            builder.Property(p => p.Description).HasColumnType("varchar(200)");
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.ToTable(p => p.HasCheckConstraint
            ("DurationDaysConstraint", "DurationDays between 1 and 365"));
        }
    }
}
