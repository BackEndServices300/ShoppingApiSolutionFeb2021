using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("curbsideorders")]
        public async Task<ActionResult<GetCurbsideDetailsResponse>> PlaceOrder(
            [FromBody] PostCurbsideRequest request
            )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(request);
        }

        [HttpGet("curbsideorders/{id:int}", Name = "curbside#getbyid")]
        public async Task<ActionResult<GetCurbsideDetailsResponse>> GetById(int id)
        {
            return Ok();
        }

        [HttpGet("curbsideorders")]
        public async Task<ActionResult<CollectionBase<GetCurbsideDetailsResponse>>> GetAll()
        {
            return Ok();
        }
    }
}
