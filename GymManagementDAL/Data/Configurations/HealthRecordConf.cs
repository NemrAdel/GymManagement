using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class HealthRecordConf : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("HealthRecords").HasKey(x=>x.Id);

            builder.HasOne<Member>().WithOne(m => m.HealthRecord)
                .HasForeignKey<Member>(m=>m.Id);

            builder.Ignore(h => h.CreatedAt);

            builder.Property(h => h.UpdatedAt).HasColumnName("LastUpdate");

        }
    }
}
