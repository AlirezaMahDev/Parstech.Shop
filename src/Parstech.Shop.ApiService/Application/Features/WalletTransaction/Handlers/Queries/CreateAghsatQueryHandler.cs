using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
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



            FecilitiesDto req=new FecilitiesDto();
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
}
