using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Commands;

public class CreateWalletTransactionCommandHandler : IRequestHandler<CreateWalletTransactionCommandReq, WalletTransactionResult>
{
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IWalletRepository _walletRep;
    private readonly IMapper _mapper;

    public CreateWalletTransactionCommandHandler(IWalletTransactionRepository walletTransactionRep,
        IMapper mapper,
        IWalletRepository walletRep)
    {
        _walletTransactionRep = walletTransactionRep;
        _mapper = mapper;
        _walletRep = walletRep;
    }
    public async Task<WalletTransactionResult> Handle(CreateWalletTransactionCommandReq request, CancellationToken cancellationToken)
    {
        WalletTransactionResult res=new();
        if (request.admin)
        {
            if(request.TransactionDto.Active==null)
            {
                var item = _mapper.Map<Parstech.Shop.Context.Domain.Models.WalletTransaction>(request.TransactionDto);
                item.TrackingCode = CodeGenerator.GenerateUniqCode();
                item.CreateDate = DateTime.Now;
                var result = await _walletTransactionRep.AddAsync(item);
                var resultDto = _mapper.Map<WalletTransactionDto>(result);
                await _walletRep.WalletCalculateTransaction(resultDto);
                res.walletTransaction = resultDto;
                res.isSuccessed = true;
                return res;
            }
            else
            {
                if (await _walletTransactionRep.WalletIsWerifyForNewFecilities(request.TransactionDto.WalletId, request.TransactionDto.TypeId, request.TransactionDto.Type))
                {
                    var item = _mapper.Map<Parstech.Shop.Context.Domain.Models.WalletTransaction>(request.TransactionDto);
                    item.TrackingCode = CodeGenerator.GenerateUniqCode();
                    item.CreateDate = DateTime.Now;
                    var result = await _walletTransactionRep.AddAsync(item);
                    var resultDto = _mapper.Map<WalletTransactionDto>(result);
                    await _walletRep.WalletCalculateTransaction(resultDto);
                    res.walletTransaction = resultDto;
                    res.isSuccessed = true;
                    return res;
                }
                else
                {
                    res.walletTransaction = null;
                    res.isSuccessed = false;
                    return res;
                }
            }

            //await _walletRep.WalletCalculateTransaction(request.TransactionDto);

        }
        else
        {
            var item = _mapper.Map<Parstech.Shop.Context.Domain.Models.WalletTransaction>(request.TransactionDto);
            item.TrackingCode = CodeGenerator.GenerateUniqCode();
            item.CreateDate = DateTime.Now;
            var result = await _walletTransactionRep.AddAsync(item);
            var resultDto = _mapper.Map<WalletTransactionDto>(result);
            //await _walletRep.WalletCalculateTransaction(resultDto);
            res.walletTransaction = resultDto;
            res.isSuccessed = true;
            return res;
        }

            
        //return resultDto;
    }
}