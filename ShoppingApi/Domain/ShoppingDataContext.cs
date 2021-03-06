﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Domain
{
    public class ShoppingDataContext : DbContext
    {
        public ShoppingDataContext(DbContextOptions<ShoppingDataContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CurbsideOrder> CurbsideOrders { get; set; }

        public IQueryable<Product> GetProductsInInventory()
        {
            return Products.Where(p => p.Available);
        }
    }
}
