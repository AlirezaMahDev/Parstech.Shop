using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Queries;

public class
    GetTransactionByParentIdQueryHandler : IRequestHandler<GetTransactionByParentIdQueryReq, List<WalletTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly IWalletTransactionRepository _walletTransactionRep;

    public GetTransactionByParentIdQueryHandler(IWalletTransactionRepository walletTransactionRep, IMapper mapper)
    {
        _walletTransactionRep = walletTransactionRep;
        _mapper = mapper;
    }

    public async Task<List<WalletTransactionDto>> Handle(GetTransactionByParentIdQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Shared.Models.WalletTransaction> list = await _walletTransactionRep.GetTransactionsByParentId(request.id);
        List<WalletTransactionDto>? dto = _mapper.Map<List<WalletTransactionDto>>(list);
        foreach (WalletTransactionDto? item in dto)
        {
            item.CreateDateShamsi = item.CreateDate.ToShamsi();
            if (item.ExpireDate != null)
            {
                item.ExpireDateShamsi = item.ExpireDate.Value.ToShamsi();
            }
        }

        return dto;
    }
}