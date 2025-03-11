using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Application.Generator;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Handlers.Commands
{
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
            WalletTransactionResult res=new WalletTransactionResult();
            if (request.admin)
            {
                if(request.TransactionDto.Active==null)
                {
                    var item = _mapper.Map<Shop.Domain.Models.WalletTransaction>(request.TransactionDto);
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
                        var item = _mapper.Map<Shop.Domain.Models.WalletTransaction>(request.TransactionDto);
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
                var item = _mapper.Map<Shop.Domain.Models.WalletTransaction>(request.TransactionDto);
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
}
