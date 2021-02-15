using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Models;
using ShoppingApi.Models.Products;


namespace ShoppingApi.Controllers
{
    public class ProductsController : ControllerBase
    {
        [HttpGet("products")]
        public ActionResult<CollectionBase<GetProductsSummaryResponse>> GetAllProducts()
        {
            var fakeResponse = new CollectionBase<GetProductsSummaryResponse>
            {
                Data = new List<GetProductsSummaryResponse>
                {
                    new GetProductsSummaryResponse { Id=1, Name="Shampoo", Price=3.99M},
                    new GetProductsSummaryResponse { Id=2, Name="Beer", Price=11.99M}
                }
            };
            return Ok(fakeResponse);
        }
    }
}
