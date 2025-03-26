using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Wallet;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Wallet.Handlers.Commands;

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
        var item = _mapper.Map<Domain.Models.Wallet>(request.walletDto);
        var Result=await _walletRep.AddAsync(item);
        return _mapper.Map<WalletDto>(Result);
    }
}