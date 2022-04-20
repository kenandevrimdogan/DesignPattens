using BasePoject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppIdentityDbContext _AppIdentityDbContext;

        public ProductRepository(AppIdentityDbContext appIdentityDbContext)
        {
            _AppIdentityDbContext = appIdentityDbContext;
        }

        public async Task<bool> Exsist(int id)
        {
            return await _AppIdentityDbContext.Products.AnyAsync(x=> x.Id == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _AppIdentityDbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAll(string userId)
        {
            return await _AppIdentityDbContext.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _AppIdentityDbContext.Products.FindAsync(id);
        }

        public async Task Remove(Product product)
        {
            _AppIdentityDbContext.Products.Remove(product);
            await _AppIdentityDbContext.SaveChangesAsync();
        }

        public async Task<Product> Save(Product product)
        {
            await _AppIdentityDbContext.Products.AddAsync(product);
            await _AppIdentityDbContext.SaveChangesAsync();

            return product;
        }

        public async Task Update(Product product)
        {
            _AppIdentityDbContext.Products.Update(product);
            await _AppIdentityDbContext.SaveChangesAsync();

        }
    }
}
