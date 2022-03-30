﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string productId);

        Task<List<Product>> GetAllUserId(string userId);

        Task<Product> Save(Product product);

        Task Update(Product product);

        Task Delete(Product product);
    }
}
