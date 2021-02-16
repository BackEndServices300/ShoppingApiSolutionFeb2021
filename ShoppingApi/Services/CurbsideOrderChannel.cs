using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class CurbsideOrderChannel
    {
        // Controller->EfOrderProcessor >========> BackgroundOrderProcess 

        private const int MaxMessageInChannel = 100;
        private readonly ILogger<CurbsideOrderChannel> _logger;
        private readonly Channel<CurbsideChannelRequest> _channel;
        public CurbsideOrderChannel(ILogger<CurbsideOrderChannel> logger)
        {
            _logger = logger;
            var options = new BoundedChannelOptions(MaxMessageInChannel)
            {
                SingleReader = true,
                SingleWriter = false
            };
            _channel = Channel.CreateBounded<CurbsideChannelRequest>(options);
        }

        // a method that allows the EfOrderProcessor to place a CurbsideChannelRequest in the channel
        public async Task<bool> AddCurbsideAsync(
            CurbsideChannelRequest order,
            CancellationToken ct = default
            )
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if(_channel.Writer.TryWrite(order))
                {
                    return true;
                } 
            }
            return false;
        }

        // a way for the BackgroundOrderProcessor to get those things, one after the other, one at a time.
        // You know, enumerate them, but in an Asynchronous way.

        public IAsyncEnumerable<CurbsideChannelRequest> ReadAllAsync(CancellationToken ct = default) =>
            _channel.Reader.ReadAllAsync();
    }
}
