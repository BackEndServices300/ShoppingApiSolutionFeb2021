using Microsoft.EntityFrameworkCore;
using ShoppingApi.Domain;
using ShoppingApi.Models;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EfProducts : ILookupProducts
    {
        private readonly ShoppingDataContext _context;

        public EfProducts(ShoppingDataContext context)
        {
            _context = context;
        }

        public async Task<CollectionBase<GetProductsSummaryResponse>> GetAllProductsInInventoryAsync()
        {
            CollectionBase<GetProductsSummaryResponse> response = new(); // C# 9 - .NET Core 5
            //var response = new CollectionBase<GetProductsSummaryResponse>();
            response.Data = await _context.Products
                .Where(p => p.Available)
                .Select(p => new GetProductsSummaryResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToListAsync();
            return response;
        }
    }
}
