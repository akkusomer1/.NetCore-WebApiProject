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
    public class CategoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");

            //builder.HasData(
            //    new Category { Id = 1, Name = "Kategori1" },
            //    new Category { Id = 2, Name = "Kategori2" },
            //    new Category { Id = 3, Name = "Kategori3" }
            //    );

        }

      
       
    }
}
