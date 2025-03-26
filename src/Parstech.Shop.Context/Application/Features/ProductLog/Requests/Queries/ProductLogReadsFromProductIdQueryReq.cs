using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductLog;

namespace Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;

public record ProductLogReadsFromProductIdQueryReq(int productId) : IRequest<LogDto>;