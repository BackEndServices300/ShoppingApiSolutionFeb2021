using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        public BackgroundOrderProcessor(ILogger<BackgroundOrderProcessor> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                _logger.LogInformation("Doing some work in teh background, yo.");
                await Task.Delay(1000, stoppingToken);
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
