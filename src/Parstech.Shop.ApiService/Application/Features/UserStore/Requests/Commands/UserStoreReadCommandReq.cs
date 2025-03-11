using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;

namespace Shop.Application.Features.UserStore.Requests.Commands
{
    public record UserStoreReadCommandReq(int id) : IRequest<UserStoreDto>;
    public record UserStoreReadsCommandReq() : IRequest<List<UserStoreDto>>;
}
