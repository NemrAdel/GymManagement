using Microsoft.EntityFrameworkCore;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class CategoryConf : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CategoryName).HasColumnType("varchar(20)");

            builder.HasMany(c => c.Sessions).WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);
                
        }
    }
}
