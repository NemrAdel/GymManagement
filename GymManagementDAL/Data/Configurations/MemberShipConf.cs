using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class MemberShipConf : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MemberShip> builder)
        {
            builder.Property(m => m.CreatedAt)
                .HasColumnName("StratDate").HasDefaultValueSql("GETDATE()");

            builder.HasKey(m => new {m.PlanId,m.MemberId });

            builder.Ignore(m => m.Status);
            builder.Ignore(m => m.Id); 
        }
    }
}
