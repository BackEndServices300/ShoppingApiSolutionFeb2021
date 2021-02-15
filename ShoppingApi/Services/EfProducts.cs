using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Domain;
using ShoppingApi.Models;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace ShoppingApi.Services
{
    public class EfProducts : ILookupProducts
    {
        private readonly ShoppingDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public EfProducts(ShoppingDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<CollectionBase<GetProductsSummaryResponse>> GetAllProductsInInventoryAsync()
        {
            CollectionBase<GetProductsSummaryResponse> response = new();
            //var response = new CollectionBase<GetProductsSummaryResponse>();
            response.Data = await _context.GetProductsInInventory()
                .ProjectTo<GetProductsSummaryResponse>(_config)
                .ToListAsync();
            return response;
        }

        public async Task<GetProductDetailsResponse> GetByIdAsync(int id)
        {
            var response = await _context.GetProductsInInventory()
                 .Where(p => p.Id == id)
                 .ProjectTo<GetProductDetailsResponse>(_config)
                 .SingleOrDefaultAsync();

            return response;
        }
    }
}
