using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class CalculateAghsatQueryHandler : IRequestHandler<CalculateAghsatQueryReq, ResponseDto>
{
    private readonly IWalletRepository _walletRep;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    public CalculateAghsatQueryHandler(IWalletRepository walletRep,
        IWalletTransactionRepository walletTransactionRep)
    {
        _walletRep = walletRep;
        _walletTransactionRep = walletTransactionRep;
    }
    public async Task<ResponseDto> Handle(CalculateAghsatQueryReq request, CancellationToken cancellationToken)
    {

        ResponseDto result = new();
        long TashilatPrice = 0;
        var transaction = await _walletTransactionRep.GetAsync(request.transactionId);

        var wallet = await _walletRep.GetAsync(transaction.WalletId);

        switch (transaction.Type)
        {
            case "Fecilities":
                TashilatPrice = wallet.Fecilities;
                break;

            case "OrgCredit":
                TashilatPrice = wallet.OrgCredit;
                break;
        }
        if (TashilatPrice == 0)
        {
            result.IsSuccessed = false;
            result.Message = "مبلغ سفارش بیشتر از میران تسهیلات شمااست";
        }
        else if (request.price > TashilatPrice )
        {
            FecilitiesDto req = new();
            req.Price = TashilatPrice;
            req.Sud = transaction.Persent.Value;
            req.GhestCount = request.month;
            var res = _walletTransactionRep.GenerateNewFesilities(req);
            result.Object = res;
            result.IsSuccessed = true;
            result.Message = "اقساط قابل پرداخت سفارش شما محاسبه گردید.پس از تائید درخواست جهت تسویه و پرداخت مبلغ ما به التفاوت سفارش به درگاه انتقال داده خواهید شد ";

        }
        else
        {
            FecilitiesDto req = new();
            req.Price = request.price;
            req.Sud = transaction.Persent.Value;
            req.GhestCount = request.month;
            var res = _walletTransactionRep.GenerateNewFesilities(req);
            result.Object = res;
            result.IsSuccessed = true;
            result.Message = "اقساط قابل پرداخت سفارش شما محاسبه گردید.لطفا با دقت آن را بررسی کرده و در صورت اطمینان سفارش خود را تائید فرمایید";

        }
        transaction.Month=request.month;
        await _walletTransactionRep.UpdateAsync(transaction);
        return result;
    }
}