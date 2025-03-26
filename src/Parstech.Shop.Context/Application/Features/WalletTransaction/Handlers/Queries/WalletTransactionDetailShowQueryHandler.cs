using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class WalletTransactionDetailShowQueryHandler : IRequestHandler<WalletTransactionDetailShowQueryReq, WalletTransactionDto>
{
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IMapper _mapper;

    public WalletTransactionDetailShowQueryHandler(IWalletTransactionRepository walletTransactionRep,
        IMapper mapper)
    {
        _walletTransactionRep = walletTransactionRep;
        _mapper = mapper;
    }
    public async Task<WalletTransactionDto> Handle(WalletTransactionDetailShowQueryReq request, CancellationToken cancellationToken)
    {
        var walletTransaction = await _walletTransactionRep.GetAsync(request.transactionId);
        var walletTransactionDto = _mapper.Map<WalletTransactionDto>(walletTransaction);
        walletTransactionDto.CreateDateShamsi = walletTransaction.CreateDate.ToShamsi();

        if (walletTransaction.Start != null)
        {
            if (walletTransaction.Start.Value)
            {
                walletTransactionDto.StartName = "شروع شده";

            }
            else
            {
                walletTransactionDto.StartName = "شروع نشده";
            }
        }
        else
        {
            walletTransactionDto.StartName = "-";
        }

        switch (walletTransaction.TypeId)
        {
            case 1:
                walletTransactionDto.TypeName = "واریز";
                break;
            case 2:
                walletTransactionDto.TypeName = "برداشت";
                break;
            case 3:
                walletTransactionDto.TypeName = "در انتظار پرداخت";
                break;
            case 4:
                walletTransactionDto.TypeName = "پرداخت موفقیت آمیز";
                break;
        }
        if (walletTransaction.ExpireDate != null)
        {
            var ExpDateShamsi = walletTransaction.ExpireDate.Value;
            walletTransactionDto.ExpireDateShamsi = ExpDateShamsi.ToShamsi();
        }
        else
        {
            walletTransactionDto.ExpireDateShamsi = "_";
        }
        if(walletTransaction.FileName == null)
        {
            walletTransactionDto.FileName = "_";
        }

        return walletTransactionDto;
    }
}