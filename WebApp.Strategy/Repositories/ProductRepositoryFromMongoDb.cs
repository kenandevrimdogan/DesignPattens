using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDb");
            MongoClient mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("ProductDb");

            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task Delete(Product product)
        {
            await _productsCollection.DeleteOneAsync(x => x.Id == product.Id);
        }

        public async Task<List<Product>> GetAllUserId(string userId)
        {
            return await _productsCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(string productId)
        {
            return await _productsCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> Save(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return product;
        }

        public async Task Update(Product product)
        {
            await _productsCollection.FindOneAndReplaceAsync(x=> x.Id == product.Id, product);
        }
    }
}
