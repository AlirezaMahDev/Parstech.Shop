using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Categury;

namespace Shop.Application.Features.Categury.Requests.Commands
{
    public record CateguryOneReadCommandReq(int categuryId) : IRequest<CateguryDto>;
    public record CateguryReadCommandReq(string filter) : IRequest<List<CateguryDto>>;
}
