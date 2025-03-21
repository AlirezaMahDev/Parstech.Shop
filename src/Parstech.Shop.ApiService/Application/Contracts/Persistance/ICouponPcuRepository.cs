﻿using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ICouponPcuRepository : IGenericRepository<CouponPcu>
{
    Task<List<CouponPcu>> GetPCUOfCoupon(string Type, bool Status, Coupon coupon);
}