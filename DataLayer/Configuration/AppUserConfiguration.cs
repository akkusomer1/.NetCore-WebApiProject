using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.UserName).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Password).IsRequired().HasMaxLength(128);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");
            builder.HasOne(x => x.AppRole).WithMany(x=>x.AppUsers).HasForeignKey(x=>x.AppRoleId);

            //builder.HasData(
            //    new AppUser { Id = 1, UserName = "akkusomer1", Password = "12345", AppRoleId = 1 },
            //    new AppUser { Id = 2, UserName = "ahmet", Password = "12345", AppRoleId = 2 },
            //    new AppUser { Id = 3, UserName = "mehmet", Password = "12345", AppRoleId = 2 },
            //    new AppUser { Id = 4, UserName = "ali", Password = "12345", AppRoleId = 2 },
            //    new AppUser { Id = 5, UserName = "ayse", Password = "12345", AppRoleId = 1 }

            //    );
        }
    }
}
