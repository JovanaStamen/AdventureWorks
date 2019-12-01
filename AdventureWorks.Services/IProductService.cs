using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using System;
using System.Collections.Generic;

namespace AdventureWorks.Services
{
    public interface IProductService
    {
        PagedResult<Product> GetProductsBySellingStartDate(int pageSize, int currentPage, DateTime sellingStartDate);

        PagedResult<Product> GetProductsByName(int pageSize, int currentPage, string name);
    }
}