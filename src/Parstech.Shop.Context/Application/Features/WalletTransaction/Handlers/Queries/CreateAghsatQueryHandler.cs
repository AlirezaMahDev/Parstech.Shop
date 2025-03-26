using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class CreateAghsatQueryHandler : IRequestHandler<CreateAghsatQueryReq, bool>
{
    private IWalletTransactionRepository _walletTransactionRep;
    private IWalletRepository _walletRep;
    private IMediator _mediator;

    public CreateAghsatQueryHandler(IWalletTransactionRepository walletTransactionRep, IMediator mediator, IWalletRepository walletRep)
    {
        _walletTransactionRep = walletTransactionRep;
        _mediator = mediator;
        _walletRep = walletRep;
    }
    public async Task<bool> Handle(CreateAghsatQueryReq request, CancellationToken cancellationToken)
    {
        var transaction =await _walletTransactionRep.GetAsync(request.transactionId);
        var wallet =await _walletRep.GetAsync(transaction.WalletId);


        long walletAmount = 0;
        long price = 0;
        switch (transaction.Type)
        {
            case "Fecilities": walletAmount = wallet.Fecilities; break;
            case "OrgCredit": walletAmount = wallet.OrgCredit; break;
        }

        if (walletAmount<request.order.Total  )
        {
            price = walletAmount;
        }
        else
        {
            price = request.order.Total;
        }



        FecilitiesDto req=new();
        req.Price = price;
        req.Sud = transaction.Persent.Value;
        req.GhestCount = request.month.Value;
        req.Karmozd = 2;
        req.WalletId = transaction.WalletId;
        req.Type = transaction.Type;
        req.OrderCode = request.order.OrderCode;
        req.ParentFecilitiesId = transaction.Id;
            
        var finalReq = _walletTransactionRep.GenerateNewFesilities(req);
        await _walletTransactionRep.CreateNewFesilities(finalReq);

        transaction.Start = true;
        await _walletTransactionRep.UpdateAsync(transaction);
        await _mediator.Send(new EditStartOrActiveTransactionQueryReq(transaction.Id, "start", true, null));
        return true;
    }
}