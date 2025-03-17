using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Generator;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Commands;

public class
    CreateWalletTransactionCommandHandler : IRequestHandler<CreateWalletTransactionCommandReq, WalletTransactionResult>
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

    public async Task<WalletTransactionResult> Handle(CreateWalletTransactionCommandReq request,
        CancellationToken cancellationToken)
    {
        WalletTransactionResult res = new();
        if (request.admin)
        {
            if (request.TransactionDto.Active == null)
            {
                Domain.Models.WalletTransaction? item =
                    _mapper.Map<Domain.Models.WalletTransaction>(request.TransactionDto);
                item.TrackingCode = CodeGenerator.GenerateUniqCode();
                item.CreateDate = DateTime.Now;
                Domain.Models.WalletTransaction result = await _walletTransactionRep.AddAsync(item);
                WalletTransactionDto? resultDto = _mapper.Map<WalletTransactionDto>(result);
                await _walletRep.WalletCalculateTransaction(resultDto);
                res.walletTransaction = resultDto;
                res.isSuccessed = true;
                return res;
            }
            else
            {
                if (await _walletTransactionRep.WalletIsWerifyForNewFecilities(request.TransactionDto.WalletId,
                        request.TransactionDto.TypeId,
                        request.TransactionDto.Type))
                {
                    Domain.Models.WalletTransaction? item =
                        _mapper.Map<Domain.Models.WalletTransaction>(request.TransactionDto);
                    item.TrackingCode = CodeGenerator.GenerateUniqCode();
                    item.CreateDate = DateTime.Now;
                    Domain.Models.WalletTransaction result = await _walletTransactionRep.AddAsync(item);
                    WalletTransactionDto? resultDto = _mapper.Map<WalletTransactionDto>(result);
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
            Domain.Models.WalletTransaction?
                item = _mapper.Map<Domain.Models.WalletTransaction>(request.TransactionDto);
            item.TrackingCode = CodeGenerator.GenerateUniqCode();
            item.CreateDate = DateTime.Now;
            Domain.Models.WalletTransaction result = await _walletTransactionRep.AddAsync(item);
            WalletTransactionDto? resultDto = _mapper.Map<WalletTransactionDto>(result);
            //await _walletRep.WalletCalculateTransaction(resultDto);
            res.walletTransaction = resultDto;
            res.isSuccessed = true;
            return res;
        }


        //return resultDto;
    }
}