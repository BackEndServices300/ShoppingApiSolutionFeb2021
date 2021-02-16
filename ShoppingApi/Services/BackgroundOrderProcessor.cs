using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class BackgroundOrderProcessor : BackgroundService
    {
        private readonly ILogger<BackgroundOrderProcessor> _logger;
        private readonly CurbsideOrderChannel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemTime _systemTime;

        public BackgroundOrderProcessor(ILogger<BackgroundOrderProcessor> logger, CurbsideOrderChannel channel, IServiceProvider serviceProvider, ISystemTime systemTime)
        {
            _logger = logger;
            _channel = channel;
            _serviceProvider = serviceProvider;
            _systemTime = systemTime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting for things on the channel...");
            // check for any unprocessed stuff and process it, THEN start processing new things.
           await foreach(var order in _channel.ReadAllAsync())
            {
                //order.CurbsideOrderId
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ShoppingDataContext>();

                var savedOrder = await context.CurbsideOrders.SingleOrDefaultAsync(o => o.Id == order.CurbsideOrderId);
                if(savedOrder == null)
                {
                    continue;
                } else
                {
                    var numberOfItems = savedOrder.Items.Split(',').Count();
                    for(var t = 0; t< numberOfItems; t++)
                    {
                        _logger.LogInformation($"Processing Item {t + 1} or order {savedOrder.Id}");
                        await Task.Delay(1000);
                    }
                    savedOrder.PickupTimeAssigned = _systemTime.GetCurrent().AddHours(numberOfItems);
                    await context.SaveChangesAsync();
                }
                
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
