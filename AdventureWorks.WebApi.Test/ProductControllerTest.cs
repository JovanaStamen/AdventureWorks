using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.Services;
using AdventureWorks.Services.ViewModels;
using AdventureWorks.WebAPI.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AdventureWorks.WebApi.Test
{
    [TestFixture]
    public class ProductControllerTest
    {
        private readonly IProductService productService = Substitute.For<IProductService>();
        List<DAL.Product> listProducts = new List<DAL.Product>();

        public void Setup()
        {
            DAL.Product product = new DAL.Product { ProductID = 1, ProductNumber = "123", Name = "Jojo", ListPrice = 2, ModifiedDate = DateTime.Now, Weight =3, DaysToManufacture=3, WeightUnitMeasureCode ="test code", ProductLine= "line test", Color = "blue", ProductModelID =2, Class = "first", SellStartDate = DateTime.Now, SellEndDate = DateTime.Now, StandardCost = 2, FinishedGoodsFlag = false, MakeFlag = true, ProductModel =  new DAL.ProductModel(), ReorderPoint = 1, rowguid =  new Guid(), SafetyStockLevel= 1, Size="Big", SizeUnitMeasureCode="123", Style="style" };
            listProducts.Add(product);

            Mapper.CreateMap<Product, ProductVM>();
            Mapper.CreateMap<ProductDescription, ProductDescriptionVM>();
        }
            
        [Test]
        public void GetByName()
        {
            this.Setup();
            _ = productService.GetProductsByName(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(new DAL.GenericRepository.PagedResult<DAL.Product> { CurrentPage = 1, PageSize = 2, PageCount = 2, Results = listProducts, Total = 1 });
            ProductController productController = new ProductController(productService);
            IHttpActionResult result = productController.GetByName("jo", 1, 2);
            var contentResult = result as OkNegotiatedContentResult<PagedResult<ProductVM>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Results[0].ProductID);
        }
    }
}
