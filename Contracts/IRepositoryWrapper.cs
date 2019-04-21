using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        //IUserRepository Recipt { get; }
        IProductRepository Product { get; }
    }
}
