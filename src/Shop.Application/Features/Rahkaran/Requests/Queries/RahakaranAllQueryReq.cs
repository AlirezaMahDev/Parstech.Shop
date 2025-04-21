using MediatR;
using Shop.Application.DTOs.Rahkaran;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Rahkaran.Requests.Queries
{
    public record RahakaranAllQueryReq(int orderId):IRequest<RahkaranAllDto>;

}
