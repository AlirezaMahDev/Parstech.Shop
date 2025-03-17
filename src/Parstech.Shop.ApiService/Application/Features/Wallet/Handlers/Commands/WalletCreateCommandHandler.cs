using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Handlers.Commands;

public class WalletCreateCommandHandler : IRequestHandler<WalletCreateCommandReq, WalletDto>
{
    private IWalletRepository _walletRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public WalletCreateCommandHandler(IWalletRepository walletRep, IMapper mapper, IMediator madiiator)
    {
        _walletRep = walletRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<WalletDto> Handle(WalletCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Wallet? item = _mapper.Map<Shared.Models.Wallet>(request.walletDto);
        Shared.Models.Wallet Result = await _walletRep.AddAsync(item);
        return _mapper.Map<WalletDto>(Result);
    }
}