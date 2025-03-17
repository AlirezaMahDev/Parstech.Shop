using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

public record PropertiesSearchQueryReq(int categuryId, int PropertCateguriId, string Filter)
    : IRequest<List<PropertyDto>>;