using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Command;

public record CreateUserProductCommandReq(string userName, int productId, string type) : IRequest<bool>;