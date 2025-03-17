using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

//if user is admin storeId=0
public record GetChildsAndProductStocksQueryReq(int productId, int storeId) : IRequest<ChildsAndStock>;