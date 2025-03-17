using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Handlers.Queries;

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
        Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(request.userId);
        return _mapper.Map<WalletDto>(wallet);
    }
}