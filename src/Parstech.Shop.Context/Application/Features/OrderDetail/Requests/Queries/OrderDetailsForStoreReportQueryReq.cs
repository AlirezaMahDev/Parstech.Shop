using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailsForStoreReportQueryReq(SalesParameterDto parameter,bool Admin) : IRequest<SalesPagingDto>;