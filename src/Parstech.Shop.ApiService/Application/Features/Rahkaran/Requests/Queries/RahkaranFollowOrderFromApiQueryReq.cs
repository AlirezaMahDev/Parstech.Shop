using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Rahkaran.Requests.Queries
{
    public record RahkaranFollowOrderFromApiQueryReq(int orderId):IRequest<ResponseDto>;

}
