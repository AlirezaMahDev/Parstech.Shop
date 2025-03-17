using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Tax.Requests.Commands;

public record TaxReadCommandReq(int id) : IRequest<TaxDto>;

public record TaxReadsCommandReq() : IRequest<List<TaxDto>>;