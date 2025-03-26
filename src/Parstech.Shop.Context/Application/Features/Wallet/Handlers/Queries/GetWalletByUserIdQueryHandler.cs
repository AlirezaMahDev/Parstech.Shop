using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Wallet;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Wallet.Handlers.Queries;

public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQueryReq, WalletDto>
{
    private readonly IWalletRepository _walletRep;
    private readonly IMapper _mapper;
    public GetWalletByUserIdQueryHandler(IWalletRepository walletRep,
        IMapper mapper)
    {
        _walletRep = walletRep;
        _mapper = mapper;
    }
    public async Task<WalletDto> Handle(GetWalletByUserIdQueryReq request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRep.GetWalletByUserId(request.userId);
        return _mapper.Map<WalletDto>(wallet);
                
    }
}