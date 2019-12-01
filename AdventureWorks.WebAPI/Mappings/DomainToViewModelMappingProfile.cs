using AdventureWorks.DAL;
using AdventureWorks.Services.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.WebAPI.Mappings
{
    public class DomainToViewModelMappingProfile: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, ProductVM>();


            Mapper.CreateMap<ProductDescription, ProductDescriptionVM>();

        }
    }
}