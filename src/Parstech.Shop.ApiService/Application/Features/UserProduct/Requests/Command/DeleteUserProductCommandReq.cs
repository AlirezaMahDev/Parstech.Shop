using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Command;

public record DeleteUserProductCommandReq(int userProductId) : IRequest;