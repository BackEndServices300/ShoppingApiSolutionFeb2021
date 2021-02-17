using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ShoppingApi.Models.Curbside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Hubs
{
    public class ShoppingHub : Hub
    {
        /*
         * PlaceOrder(PostCurbsideRequest) -> places order, and returns OrderPlaced
         */
        private readonly ILogger<ShoppingHub> _logger;
        private readonly IProcessCurbsideOrders _orderProcessor;

        public ShoppingHub(ILogger<ShoppingHub> logger, IProcessCurbsideOrders orderProcessor)
        {
            _logger = logger;
            _orderProcessor = orderProcessor;
        }

        public async Task PlaceOrder(PostCurbsideRequest request)
        { 
            // no model validation! You have validate manually, or whatever.
            _logger.LogInformation("Got an order" + request.PickupPerson);
            var response = await _orderProcessor.PlaceOrderAsync(request);
            await Clients.Caller.SendAsync("OrderPlaced", response);
        }
    }
}
