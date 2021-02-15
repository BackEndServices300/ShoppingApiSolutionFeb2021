using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Profiles
{
    public class ProductProfiles : Profile
    {
        private readonly ISystemTime _systemTime;

        public ProductProfiles(ISystemTime systemTime)
        {
            _systemTime = systemTime;
            // Product -> GetProductsSummaryResponse
            CreateMap<Product, GetProductsSummaryResponse>();

            CreateMap<Product, GetProductDetailsResponse>()
                .ForMember(dest => dest.NumberInInventory,
                     options => options.MapFrom(src => src.NumberAvailable))
                .ForMember(dest => dest.DaysInInventory,
                     options => options.MapFrom(src => (_systemTime.GetCurrent() - src.AddedToInventoryOn).Days));
        }

        
    }
}
