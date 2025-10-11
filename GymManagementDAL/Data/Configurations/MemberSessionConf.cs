using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class MemberSessionConf : IEntityTypeConfiguration<MemberSessions>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MemberSessions> builder)
        {
            builder.Property(ms => ms.CreatedAt)
                .HasColumnName("BookingDate").HasDefaultValueSql("GETDATE()");
            builder.Ignore(ms => ms.Id);
            builder.HasKey(ms => new { ms.MemberId, ms.SessionId });
        }
    }
}
