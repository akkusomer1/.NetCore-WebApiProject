using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configuration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).IsRequired();
            builder.Property(x=>x.UpdateDate).IsRequired(false);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");

            //builder.HasData(
            //    new AppRole { Id = 1, RoleName = "Admin" },
            //    new AppRole { Id = 2, RoleName = "User" }
            //    );
        }
    }
}
