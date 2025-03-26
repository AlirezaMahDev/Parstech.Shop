using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductRelated;

namespace Parstech.Shop.Context.Application.Features.ProductRelated.Requests.Commnads;

public record CreateProductRelatedCommandReq(ProductRelatedDto productRelatedDto) :IRequest;