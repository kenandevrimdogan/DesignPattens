using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Decorator.Models;
using WebApp.Decorator.Repositories;

namespace WebApp.Decorator.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;
        private const string ProductCacheName = "products";

        public ProductRepositoryCacheDecorator(IProductRepository repository, IMemoryCache memoryCache) : base(repository)
        {
            _memoryCache = memoryCache;
        }

        private List<Product> GetMemoryCache()
        {
            return _memoryCache.Get<List<Product>>(ProductCacheName);
        }

        private async Task UpdateCache()
        {
            _memoryCache.Set(ProductCacheName, await base.GetAll());
        }

        public override async Task<List<Product>> GetAll()
        {
            if (_memoryCache.TryGetValue(ProductCacheName, out List<Product> cacheProducts))
            {
                return cacheProducts;
            }

            await UpdateCache();

            return GetMemoryCache();
        }

        public override async Task<List<Product>> GetAll(string userId)
        {
            var products = await GetAll();

            return products.Where(x => x.UserId == userId).ToList();
        }

        public override async Task<Product> Save(Product product)
        {
            await base.Save(product);

            await UpdateCache();

            return product;
        }

        public override async Task Update(Product product)
        {
            await base.Update(product);

            await UpdateCache();
        }

        public override async Task Remove(Product product)
        {
            await base.Remove(product);

            await UpdateCache();
        }
    }
}
