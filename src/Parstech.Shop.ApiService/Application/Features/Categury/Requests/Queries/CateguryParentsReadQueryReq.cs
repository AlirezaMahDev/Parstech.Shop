using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Categury;

namespace Shop.Application.Features.Categury.Requests.Queries
{
    public record CateguryParentsReadQueryReq() : IRequest<List<CateguryDto>>;
}
