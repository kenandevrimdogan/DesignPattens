using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Decorator.Models;
using WebApp.Decorator.Repositories;

namespace WebApp.Decorator.Decorator
{
    public abstract class BaseProductRepositoryDecorator : IProductRepository
    {
        public readonly IProductRepository _repository;

        public BaseProductRepositoryDecorator(IProductRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> Exsist(int id)
        {
            return await _repository.Exsist(id);
        }

        public virtual async Task<List<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<List<Product>> GetAll(string userId)
        {
            return await _repository.GetAll(userId);
        }

        public virtual async Task<Product> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public virtual async Task Remove(Product product)
        {
            await _repository.Remove(product);
        }

        public virtual async Task<Product> Save(Product product)
        {
            return await _repository.Save(product);
        }

        public virtual async Task Update(Product product)
        {
           await _repository.Update(product);
        }
    }
}
