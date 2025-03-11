using MediatR;
using Shop.Application.DTOs.Categury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Categury.Requests.Queries
{
    public record GetCateguryByLatinameQueryReq(string latinName):IRequest<CateguryDto>;

}
