using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_commerce_web.model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.data
{
    public class ProductDataManager
    {
        private readonly ECommerceDbContext context;

        public ProductDataManager(ECommerceDbContext context) { 
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddNewProductAsync(Product product)
        {
            await this.context.Products.AddAsync(product);

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await this.context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();
        }
    
        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId)
        {

            return await this.context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();
        }


    }
}
