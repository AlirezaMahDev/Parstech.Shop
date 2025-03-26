using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class EditStartOrActiveTransactionQueryHandler : IRequestHandler<EditStartOrActiveTransactionQueryReq>
{
        
    private readonly IMapper _mapper;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    public EditStartOrActiveTransactionQueryHandler(
        IWalletTransactionRepository walletTransactionRep,
        IMapper mapper)
    {
            
        _mapper=mapper;
        _walletTransactionRep=walletTransactionRep;
    }
    public  async Task Handle(EditStartOrActiveTransactionQueryReq request, CancellationToken cancellationToken)
    {
        var Transaction =await _walletTransactionRep.GetAsync(request.transactionId);
            
        switch (request.startOrActive)
        {
            case "start":
                Transaction.Start = request.start;
                break;
            case "active":
                Transaction.Active = request.active;
                break;
            case "both":
                Transaction.Start = request.start;
                Transaction.Active = request.active;
                break;
            default:break;
        }
        var item = _mapper.Map<Domain.Models.WalletTransaction>(Transaction);
        await _walletTransactionRep.UpdateAsync(item);
    }
}