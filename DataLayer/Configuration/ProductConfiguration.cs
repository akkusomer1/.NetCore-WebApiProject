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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);

            //builder.HasData(

            //    new Product { Id = 1, Name = "Urun1", Price = 20, Stock = 50, CategoryId = 1 },
            //    new Product { Id = 2, Name = "Urun2", Price = 30, Stock = 50, CategoryId = 2 },
            //    new Product { Id = 3, Name = "Urun3", Price = 40, Stock = 50, CategoryId = 1 },
            //    new Product { Id = 4, Name = "Urun4", Price = 50, Stock = 50, CategoryId = 3 },
            //    new Product { Id = 5, Name = "Urun5", Price = 60, Stock = 50, CategoryId = 3 }
            //    );
        }
    }
}
