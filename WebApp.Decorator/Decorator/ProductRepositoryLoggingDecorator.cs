using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Decorator.Models;
using WebApp.Decorator.Repositories;

namespace WebApp.Decorator.Decorator
{
    public class ProductRepositoryLoggingDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductRepositoryLoggingDecorator> _logger;

        public ProductRepositoryLoggingDecorator(IProductRepository repository, ILogger<ProductRepositoryLoggingDecorator> logger) : base(repository)
        {
            _logger = logger;
        }

        public override async Task<List<Product>> GetAll()
        {
            _logger.LogInformation("GetAll is start");
            return await base.GetAll();
        }

        public override async Task<List<Product>> GetAll(string userId)
        {
            _logger.LogInformation("GetAll(userId) is start");
            return await base.GetAll(userId);
        }

        public override async Task<Product> Save(Product product)
        {
            _logger.LogInformation("Save is start");
            return await base.Save(product);
        }

        public override async Task Update(Product product)
        {
            _logger.LogInformation("Update is start");
            await base.Update(product);
        }

        public override async Task Remove(Product product)
        {
            _logger.LogInformation("Remove is start");
            await base.Remove(product);
        }
    }
}
