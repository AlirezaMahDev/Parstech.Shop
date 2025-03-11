using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.UserStore;

namespace Shop.Application.Features.UserStore.Requests.Queries
{
    public record StoreListQueryReq() : IRequest<List<UserStoreDto>>;

}
