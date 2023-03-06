using CoreLayer.Entities;
using CoreLayer.Interfaces.Repositories;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductWithCategoryAsync()
        {
            return await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
        }
    }
}
