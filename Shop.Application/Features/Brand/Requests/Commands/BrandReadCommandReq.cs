using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.ProductType;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;

namespace Shop.Application.Features.Brand.Requests.Commands
{
    public record BrandReadCommandReq(int id) : IRequest<BrandDto>;
    public record BrandReadsCommandReq() : IRequest<List<BrandDto>>;
}
