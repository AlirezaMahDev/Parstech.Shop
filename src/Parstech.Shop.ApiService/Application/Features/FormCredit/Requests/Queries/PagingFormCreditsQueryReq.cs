using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Queries;

public record PagingFormCreditsQueryReq(ParameterDto Parameter) : IRequest<List<FormCreditDto>>;