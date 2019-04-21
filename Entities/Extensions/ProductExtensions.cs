using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class ProductExtensions
    {
        public static void Map(this Product dbProduct, Product product)
        {
            dbProduct.Name = product.Name;
        }
    }
}
