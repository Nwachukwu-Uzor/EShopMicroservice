using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            var couponModel = coupon.Adapt<CouponModel>();
            logger.LogInformation("Successfully created Discount for Product Name: {productName}, Amount: {amount}",
                coupon.ProductName, coupon.Amount);
            return couponModel;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);
            if (coupon is null)
            {
                coupon = new Coupon()
                {   
                    Amount = 0,
                    ProductName = "No Discount",
                    Description = "No Discount"
                };
            }
            logger.LogInformation("Discount retrieved for Product Name: {productName}, Amount: {amount}",
                coupon.ProductName, coupon.Amount);
            var response = coupon.Adapt<CouponModel>();
            return response;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var existingCoupon = await dbContext.Coupons.FindAsync(request.Coupon.Id);
            if (existingCoupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product Not Found"));
            }
            var couponData = request.Adapt<Coupon>();
            couponData.Adapt(existingCoupon);
            dbContext.Coupons.Update(existingCoupon);
            await dbContext.SaveChangesAsync();
            var couponModel = existingCoupon.Adapt<CouponModel>();
            logger.LogInformation("Discount updated for Product Name: {productName}, Amount: {amount}",
                existingCoupon.ProductName, existingCoupon.Amount);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var existingCoupon = await dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);
            if (existingCoupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product Not Found"));
            }
            dbContext.Remove(existingCoupon);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse() {IsSuccess = true};
        }
    }
}
