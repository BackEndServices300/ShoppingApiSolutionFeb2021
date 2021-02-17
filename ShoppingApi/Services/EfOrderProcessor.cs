using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
        private readonly MapperConfiguration _config;
        private readonly ISystemTime _systemTime;
        private readonly CurbsideOrderChannel _channel;

        public EfOrderProcessor(ShoppingDataContext context, IMapper mapper, ISystemTime systemTime, MapperConfiguration config, CurbsideOrderChannel channel)
        {
            _context = context;
            _mapper = mapper;
            _systemTime = systemTime;
            _config = config;
            _channel = channel;
        }

        public async Task<GetCurbsideDetailsResponse> GetByIdAsync(int id)
        {
            var response = await _context.CurbsideOrders
                 .Where(o => o.Id == id)
                 .ProjectTo<GetCurbsideDetailsResponse>(_config)
                 .SingleOrDefaultAsync();

            return response;
        }

        public async Task<GetCurbsideDetailsResponse> PlaceOrderAsync(PostCurbsideRequest request, string wsClientId = null)
        {


            var orderToSave = _mapper.Map<CurbsideOrder>(request);
            _context.CurbsideOrders.Add(orderToSave);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetCurbsideDetailsResponse>(orderToSave);
            var didWrite = await _channel.AddCurbsideAsync(
                new CurbsideChannelRequest
                {
                    CurbsideOrderId = response.Id,
                    WsConnectionId = wsClientId
                }
                );
            if (!didWrite)
            {
                // let's talk about this after we see that it works.
            }
            return response;

        }

        public async Task<GetCurbsideDetailsResponse> PlaceOrderNoBGAsync(PostCurbsideRequest request)
        {
            var orderToSave = _mapper.Map<CurbsideOrder>(request);
            var numberOfItems = orderToSave.Items.Split(',').Count();
            await Task.Delay(numberOfItems * 1000);
            orderToSave.PickupTimeAssigned = _systemTime.GetCurrent().AddHours(numberOfItems);
            _context.CurbsideOrders.Add(orderToSave);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetCurbsideDetailsResponse>(orderToSave);

            return response;
        }
    }
}
