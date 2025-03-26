using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record GetSomeOfLastProductsByCateguryIdQueryReq(int Take,int CateguryId):IRequest<List<ProductListShowDto>>;