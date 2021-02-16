using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Curbside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Profiles
{
    public class CurbsideOrdersProfile : Profile
    {
        public CurbsideOrdersProfile()
        {
            //  - PostCurbsideRequest -> CurbsideOrder
            CreateMap<PostCurbsideRequest, CurbsideOrder>();

            //  - CurbsideOrder -> GetCurbsideDetailsResponse
            CreateMap<CurbsideOrder, GetCurbsideDetailsResponse>();
        }
    }
}
