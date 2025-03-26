using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

public record UniqProductRepresentationsOfRepReadQueryReq(int repId) : IRequest<ProductRepresentationList>;