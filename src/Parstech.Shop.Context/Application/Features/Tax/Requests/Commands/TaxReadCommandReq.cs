using MediatR;

using Parstech.Shop.Context.Application.DTOs.Tax;

namespace Parstech.Shop.Context.Application.Features.Tax.Requests.Commands;

public record TaxReadCommandReq(int id) : IRequest<TaxDto>;
public record TaxReadsCommandReq() : IRequest<List<TaxDto>>;