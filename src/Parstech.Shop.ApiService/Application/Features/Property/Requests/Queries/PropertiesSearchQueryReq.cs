using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

public record PropertiesSearchQueryReq(int categuryId, int PropertCateguriId, string Filter)
    : IRequest<List<PropertyDto>>;