using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Grpc.Configuration;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasData(
            new Coupon()
            {
                Id = 1,
                ProductName = "IPhone X",
                Description = "IPhone Discount",
                Amount = 150
            },
            new Coupon() {
                Id = 2,
                ProductName = "Samsung 10",
                Description = "Samsung Discount",
                Amount = 100
            }
        );
    }
}