using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateProduct(Product product)
        {
            Create(product);
            Save();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return FindAll()
                .OrderBy(p => p.Name);
        }

        public Product GetProductById(int productId)
        {
            return FindByCondiftion(p => p.Id.Equals(productId))
                .FirstOrDefault();
        }

        public void UpdateProduct(Product dbProduct, Product product)
        {
            dbProduct.Map(product);
            Update(dbProduct);
            Save();
        }
    }
}
