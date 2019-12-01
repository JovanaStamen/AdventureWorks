using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.Services;
using AdventureWorks.Services.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdventureWorks.WebAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [Route("name/{name}")]
        public IHttpActionResult GetByName([FromUri] string name, [FromUri] int offset, [FromUri] int count)
        {
            var pagedProducts = productService.GetProductsByName(count,offset, name);
            PagedResult<ProductVM> productsVM = new PagedResult<ProductVM> {CurrentPage = pagedProducts.CurrentPage, PageSize = pagedProducts.PageSize, Results = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductVM>>(pagedProducts.Results).ToList() };
            if (productsVM.Results != null && productsVM.Results.Count == 0)
            {
                return NotFound();
            }

            return Ok(productsVM);

        }

        [Route("startDate/{startDate}")]
        public IHttpActionResult GetByStartDate([FromUri] string startDate, [FromUri] int offset, [FromUri] int count)
        {
            DateTime.TryParse(startDate, out DateTime result);
            var pagedProducts = productService.GetProductsBySellingStartDate(count, offset, result);
            PagedResult<ProductVM> productsVM = new PagedResult<ProductVM> { CurrentPage = pagedProducts.CurrentPage, PageSize = pagedProducts.PageSize, Results = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductVM>>(pagedProducts.Results).ToList() };

            if (productsVM.Results != null && productsVM.Results.Count == 0)
            {
                return NotFound();
            }

            return Ok(productsVM);

        }
    }
}
