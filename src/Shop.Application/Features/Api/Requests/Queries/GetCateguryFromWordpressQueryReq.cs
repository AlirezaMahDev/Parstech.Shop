using MediatR;
using Shop.Application.DTOs.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Requests.Queries
{
    public record GetCateguryFromWordpressQueryReq(int page):IRequest<List<resultWordpress>>;

}
