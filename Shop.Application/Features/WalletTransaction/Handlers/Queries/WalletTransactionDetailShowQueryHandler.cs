using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
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
}
