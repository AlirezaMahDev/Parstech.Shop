using MediatR;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.UserStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserStore.Requests.Queries
{
    public record UserStoresForProductsListCateguryQueryReq(string Categury) : IRequest<List<UserStoreDto>>;

}
