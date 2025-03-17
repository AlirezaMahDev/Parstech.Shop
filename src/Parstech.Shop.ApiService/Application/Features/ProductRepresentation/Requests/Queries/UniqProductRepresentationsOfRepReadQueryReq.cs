using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record UniqProductRepresentationsOfRepReadQueryReq(int repId) : IRequest<ProductRepresentationList>;