using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.PropertyCategury;

namespace Shop.Application.Features.PropertyCategury.Requests.Commands
{
    public record PropertyCateguryReadCommandReq(int Id) : IRequest<PropertyCateguryDto>;
    public record PropertyCateguryReadsCommandReq() : IRequest<List<PropertyCateguryDto>>;
}
