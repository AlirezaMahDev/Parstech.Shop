using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class GetTransactionByParentIdQueryHandler : IRequestHandler<GetTransactionByParentIdQueryReq, List<WalletTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly IWalletTransactionRepository _walletTransactionRep;
        
    public GetTransactionByParentIdQueryHandler(IWalletTransactionRepository walletTransactionRep, IMapper mapper)
    {
           
        _walletTransactionRep = walletTransactionRep;
        _mapper = mapper;
            
    }
    public async Task<List<WalletTransactionDto>> Handle(GetTransactionByParentIdQueryReq request, CancellationToken cancellationToken)
    {
        var list =await _walletTransactionRep.GetTransactionsByParentId(request.id);
        var dto=_mapper.Map<List<WalletTransactionDto>>(list);
        foreach (var item in dto)
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