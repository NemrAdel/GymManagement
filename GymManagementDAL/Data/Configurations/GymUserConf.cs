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
    public class GymUserConf<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name).HasColumnType("nvarchar(50)");

            builder.Property(u => u.Email).HasColumnType("nvarchar(100)");

            builder.ToTable(t =>
            t.HasCheckConstraint("EmailConstraint", "[Email] like '_%@_%._%' "));

            builder.Property(g => g.Phone).HasColumnType("nvarchar(11)");

            builder.ToTable(t => t
            .HasCheckConstraint("PhoneConstraint", "[Phone] like '01[0125]________'"));

            builder.OwnsOne(g => g.Address, ad =>
            {
                ad.Property(a => a.Street).HasColumnType("varchar(30)");
                ad.Property(a => a.City).HasColumnType("varchar(30)");
            });

        }
    }

}
