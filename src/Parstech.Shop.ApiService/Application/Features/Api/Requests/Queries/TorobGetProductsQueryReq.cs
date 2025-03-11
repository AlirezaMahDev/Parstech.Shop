using MediatR;
using Shop.Application.DTOs.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Requests.Queries
{
    public record TorobGetProductsQueryReq(int page):IRequest<List<TorobProductDto>>;
    public record TorobGetProductQueryReq(int productId,string url):IRequest<TorobDto>;

}
