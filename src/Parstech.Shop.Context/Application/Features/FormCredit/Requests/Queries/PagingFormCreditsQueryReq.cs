using MediatR;
using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Requests.Queries;

public record PagingFormCreditsQueryReq(ParameterDto Parameter) :IRequest<List<FormCreditDto>>;