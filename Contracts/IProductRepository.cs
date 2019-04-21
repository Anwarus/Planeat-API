using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void CreateProduct(Product product);
        void UpdateProduct(Product dbProduct, Product product);
        void DeleteProduct(Product product);
    }
}
