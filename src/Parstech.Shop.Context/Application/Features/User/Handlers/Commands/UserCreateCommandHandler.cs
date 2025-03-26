using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.DTOs.Wallet;
using Parstech.Shop.Context.Application.Features.User.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Commands;

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

        WalletDto wallet = new()
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