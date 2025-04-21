using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserProduct.Requests.Query
{
    public record GetFavoriteProductOfUsersQueryReq(string userName):IRequest<List<FavoriteDto>>;

}
