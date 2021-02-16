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

        public EfOrderProcessor(ShoppingDataContext context, IMapper mapper, ISystemTime systemTime, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _systemTime = systemTime;
            _config = config;
        }

        public async Task<GetCurbsideDetailsResponse> GetByIdAsync(int id)
        {
            var response = await _context.CurbsideOrders
                 .Where(o => o.Id == id)
                 .ProjectTo<GetCurbsideDetailsResponse>(_config)
                 .SingleOrDefaultAsync();

            return response;
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
