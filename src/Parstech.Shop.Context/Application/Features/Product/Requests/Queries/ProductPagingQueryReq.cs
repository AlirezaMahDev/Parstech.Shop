﻿using MediatR;

using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

//صفحه بندی کلی
public record ProductPagingQueryReq(ProductParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
//سرچ محصول و پنل تامین کننده
public record ProductPagingSarachOrStoreQueryReq(ProductSearchParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
//دسته بندی محصولات
public record ProductPagingCateguryQueryReq(ProductSearchParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;