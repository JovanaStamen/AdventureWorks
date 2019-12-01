using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductDescription> productDescriptionRepository;
        private readonly IUnitOfWork unitOfWork;


        public ProductService(IRepository<Product> productRepository, IRepository<ProductDescription> productDescriptionRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.productDescriptionRepository = productDescriptionRepository;
            this.unitOfWork = unitOfWork;
        }

        public PagedResult<Product> GetProductsByName(int pageSize, int currentPage, string name)
        {
            IOrderedQueryable<Product> queryProducts = productRepository.Get(orderBy: q => q.OrderBy(d => d.Name), x => x.Name.Contains(name));
            PagedResultExtension extension = new PagedResultExtension();
            return extension.GetPaged<Product>(queryProducts, currentPage, pageSize);
        }

        public PagedResult<Product> GetProductsBySellingStartDate(int pageSize, int currentPage, DateTime sellingStartDate)
        {
            IOrderedQueryable<Product> queryProducts = productRepository.Get(orderBy: q => q.OrderBy(d => d.Name), x => x.SellStartDate.Equals(sellingStartDate));
            PagedResultExtension extension = new PagedResultExtension();
            return extension.GetPaged<Product>(queryProducts, currentPage, pageSize);
        }
    }
}
