using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Models;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ILookupProducts _productLookups;

        public ProductsController(ILookupProducts productLookups)
        {
            _productLookups = productLookups;
        }

        [HttpGet("products")]
        public async Task<ActionResult<CollectionBase<GetProductsSummaryResponse>>> GetAllProducts()
        {
            CollectionBase<GetProductsSummaryResponse> response = 
                await _productLookups.GetAllProductsInInventoryAsync();
            
            return Ok(response);
        }

        [HttpGet("products/{id:int}")]
        public async Task<ActionResult<GetProductDetailsResponse>> GetById(int id)
        {
            GetProductDetailsResponse response = await _productLookups.GetByIdAsync(id);
            if(response == null)
            {
                return NotFound();
            } else
            {
                return Ok(response);
            }
        } 
    }
}
