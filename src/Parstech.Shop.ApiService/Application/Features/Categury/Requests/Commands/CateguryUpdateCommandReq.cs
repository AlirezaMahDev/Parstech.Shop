using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Categury;

namespace Shop.Application.Features.Categury.Requests.Commands
{
    public record CateguryUpdateCommandReq(CateguryDto CateguryDto) : IRequest<CateguryDto>;

}
