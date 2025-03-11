using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.Wallet;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.Wallet.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandReq, UserDto>
    {
        private IUserRepository _userRep;
        private IMapper _mapper;
        private IMediator _mediator;

        public UserCreateCommandHandler(IUserRepository userRep, IMapper mapper, IMediator mediator)
        {
            _userRep = userRep;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<UserDto> Handle(UserCreateCommandReq request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Models.User>(request.userDto);
            user.LastLoginDate = DateTime.Now;
            var userResult=await _userRep.AddAsync(user);

            WalletDto wallet = new WalletDto()
            {
                UserId = userResult.Id,
                Amount = 0,
                Coin = 0,
                Fecilities = 0,
                IsBlock = false,
            };



            
            await _mediator.Send(new WalletCreateCommandReq(wallet));
            return _mapper.Map<UserDto>(userResult);
        }
    }
}
