using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

public record CateguryDeleteCommandReq(int categuryId):IRequest<ResponseDto>;