using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record CalculateAghsatQueryReq(long price,int transactionId,int month):IRequest<ResponseDto>;
}