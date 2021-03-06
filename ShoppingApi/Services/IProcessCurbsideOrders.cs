﻿using ShoppingApi.Models.Curbside;
using System.Threading.Tasks;

namespace ShoppingApi
{
    public interface IProcessCurbsideOrders
    {
        Task<GetCurbsideDetailsResponse> PlaceOrderAsync(PostCurbsideRequest request, string wsConnectionId = null);
        Task<GetCurbsideDetailsResponse> GetByIdAsync(int id);
        Task<GetCurbsideDetailsResponse> PlaceOrderNoBGAsync(PostCurbsideRequest request);
    }
}