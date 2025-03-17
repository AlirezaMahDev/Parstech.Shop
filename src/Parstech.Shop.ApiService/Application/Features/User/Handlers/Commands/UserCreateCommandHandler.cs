using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Commands;

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
        Shared.Models.User? user = _mapper.Map<Shared.Models.User>(request.userDto);
        user.LastLoginDate = DateTime.Now;
        Shared.Models.User userResult = await _userRep.AddAsync(user);

        WalletDto wallet = new()
        {
            UserId = userResult.Id,
            Amount = 0,
            Coin = 0,
            Fecilities = 0,
            IsBlock = false
        };


        await _mediator.Send(new WalletCreateCommandReq(wallet));
        return _mapper.Map<UserDto>(userResult);
    }
}