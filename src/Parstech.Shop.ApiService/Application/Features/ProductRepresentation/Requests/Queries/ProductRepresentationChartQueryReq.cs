using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record ProductRepresentationChartQueryReq(int ProductId) : IRequest<List<ProductRepresenationChartDto>>;