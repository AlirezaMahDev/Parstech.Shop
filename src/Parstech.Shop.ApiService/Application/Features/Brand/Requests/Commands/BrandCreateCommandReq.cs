using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Brand;

namespace Shop.Application.Features.Brand.Requests.Commands
{
    public record BrandCreateCommandReq(BrandDto BrandDto) : IRequest<BrandDto>;

}
