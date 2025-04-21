using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductType;
using Shop.Application.DTOs.Tax;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;

namespace Shop.Application.Features.Tax.Requests.Commands
{
    public record TaxReadCommandReq(int id) : IRequest<TaxDto>;
    public record TaxReadsCommandReq() : IRequest<List<TaxDto>>;
}
