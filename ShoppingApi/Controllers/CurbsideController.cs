using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShoppingApi.Hubs;
using ShoppingApi.Models;
using ShoppingApi.Models.Curbside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    public class CurbsideController : ControllerBase
    {
        private readonly IProcessCurbsideOrders _curbsideOrders;
        private readonly IHubContext<ShoppingHub> _hub;

        public CurbsideController(IProcessCurbsideOrders curbsideOrders, IHubContext<ShoppingHub> hub)
        {
            _curbsideOrders = curbsideOrders;
            _hub = hub;
        }

        [HttpPost("curbsideorders/sync")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
        public async Task<ActionResult<GetCurbsideDetailsResponse>> PlaceOrderSync(
            [FromBody] PostCurbsideRequest request
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                GetCurbsideDetailsResponse response = await _curbsideOrders.PlaceOrderNoBGAsync(request);
                return CreatedAtRoute("curbside#getbyid", new { id = response.Id }, response);
            }

        }

        [HttpPost("curbsideorders")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
        public async Task<ActionResult<GetCurbsideDetailsResponse>> PlaceOrder(
            [FromBody] PostCurbsideRequest request
            )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {

                GetCurbsideDetailsResponse response = await _curbsideOrders.PlaceOrderAsync(request);
                await _hub.Clients.All.SendAsync("OrderPlaced", response);
                return CreatedAtRoute("curbside#getbyid", new { id = response.Id }, response );
            }
          
        }

        [HttpGet("curbsideorders/{id:int}", Name = "curbside#getbyid")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
   
        public async Task<ActionResult<GetCurbsideDetailsResponse>> GetById(int id)
        {
            GetCurbsideDetailsResponse response = await _curbsideOrders.GetByIdAsync(id);
            if(response != null)
            {
                return Ok(response);
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("curbsideorders")]
        public async Task<ActionResult<CollectionBase<GetCurbsideDetailsResponse>>> GetAll()
        {
            return Ok();
        }
    }
}
