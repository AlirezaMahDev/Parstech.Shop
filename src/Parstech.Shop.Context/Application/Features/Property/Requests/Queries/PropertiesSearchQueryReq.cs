using MediatR;
using Parstech.Shop.Context.Application.DTOs.Property;

namespace Parstech.Shop.Context.Application.Features.Property.Requests.Queries;

public record PropertiesSearchQueryReq(int categuryId,int PropertCateguriId,string Filter) : IRequest<List<PropertyDto>>;