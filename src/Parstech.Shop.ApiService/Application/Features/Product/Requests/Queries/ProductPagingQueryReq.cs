using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{

    //صفحه بندی کلی
    public record ProductPagingQueryReq(ProductParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
    //سرچ محصول و پنل تامین کننده
    public record ProductPagingSarachOrStoreQueryReq(ProductSearchParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
    //دسته بندی محصولات
    public record ProductPagingCateguryQueryReq(ProductSearchParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
}
