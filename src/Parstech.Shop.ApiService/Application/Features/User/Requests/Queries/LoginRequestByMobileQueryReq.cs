using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Requests.Queries
{
    public record LoginRequestByMobileQueryReq(string Mobile):IRequest<ResponseDto>;

}
