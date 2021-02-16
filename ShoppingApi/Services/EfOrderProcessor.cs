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

        public EfOrderProcessor(ShoppingDataContext context)
        {
            _context = context;
        }

        public Task<GetCurbsideDetailsResponse> PlaceOrderAsync(PostCurbsideRequest request)
        {
            // Todo:
            // Set up the service for this in our ServicesCollection as a Scoped service.
            // Set up automapper for this.
            // Processing each requested item.
            //   This takes time. Each item has to be checked in inventory, etc. 
            //   A shopping list has to be produced for our CurbsideShoppers
            //   We'll fake all this - each item in the array will take 1second.
            // Save the Curbside order to the database
            //   - Map our Request to a CurbsideOrder domain object.
            //   - Add it to the context
            //   - Save it
            // Map it into a GetCurbsideDetailsResponse
            // Return.
            throw new NotImplementedException();
        }
    }
}
