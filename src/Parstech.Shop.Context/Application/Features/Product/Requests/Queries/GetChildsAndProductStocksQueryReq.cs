using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

//if user is admin storeId=0
public record GetChildsAndProductStocksQueryReq(int productId,int storeId):IRequest<ChildsAndStock>;