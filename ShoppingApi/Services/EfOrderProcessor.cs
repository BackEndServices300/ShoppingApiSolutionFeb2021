using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Curbside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EfOrderProcessor : IProcessCurbsideOrders
    {
        private readonly ShoppingDataContext _context;
        private readonly IMapper _mapper;
        private readonly ISystemTime _systemTime;

        public EfOrderProcessor(ShoppingDataContext context, IMapper mapper, ISystemTime systemTime)
        {
            _context = context;
            _mapper = mapper;
            _systemTime = systemTime;
        }

        public async Task<GetCurbsideDetailsResponse> PlaceOrderAsync(PostCurbsideRequest request)
        {

            var numberOfItems = request.Items.Split(',').Count();
            await Task.Delay(numberOfItems * 1000);
            var orderToSave = _mapper.Map<CurbsideOrder>(request);
            orderToSave.PickupTimeAssigned = _systemTime.GetCurrent().AddHours(numberOfItems);
            _context.CurbsideOrders.Add(orderToSave);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetCurbsideDetailsResponse>(orderToSave);
            //response.PickupTimeAssigned = _systemTime.GetCurrent().AddHours(numberOfItems);
            return response;
        }
    }
}
