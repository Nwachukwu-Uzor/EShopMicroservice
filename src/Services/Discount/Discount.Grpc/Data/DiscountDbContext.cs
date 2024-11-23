﻿using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
            
        }
    }
}
