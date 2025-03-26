using MediatR;

namespace Parstech.Shop.Context.Application.Features.UserProduct.Requests.Command;

public record DeleteUserProductCommandReq(int userProductId):IRequest;