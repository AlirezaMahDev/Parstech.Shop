using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductType;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;

namespace Shop.Application.Features.ProductType.Requests.Commands
{
    public record ProductTypeReadCommandReq(int id) : IRequest<ProductTypeDto>;
    public record ProductTypeReadsCommandReq() : IRequest<List<ProductTypeDto>>;
}
