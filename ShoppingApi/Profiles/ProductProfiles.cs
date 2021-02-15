using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            // Product -> GetProductsSummaryResponse
            CreateMap<Product, GetProductsSummaryResponse>();
        }
    }
}
