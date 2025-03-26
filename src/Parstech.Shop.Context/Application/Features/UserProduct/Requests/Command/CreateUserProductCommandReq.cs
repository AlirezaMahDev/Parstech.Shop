using MediatR;

namespace Parstech.Shop.Context.Application.Features.UserProduct.Requests.Command;

public record CreateUserProductCommandReq(string userName,int productId,string type):IRequest<bool>;