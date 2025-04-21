using MediatR;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Queries
{
    
    public record OrderDetailsForStoreReportQueryReq(SalesParameterDto parameter,bool Admin) : IRequest<SalesPagingDto>;
}
