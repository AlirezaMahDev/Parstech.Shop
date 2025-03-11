using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
    public class GhestPaymentQueryHandler : IRequestHandler<GhestPaymentQueryReq, ResponseDto>
    {
        private readonly IWalletRepository _walletRep;
        private readonly IWalletTransactionRepository _walletTransactionRep;
        private readonly IUserRepository _userRep;
        private readonly IUserBillingRepository _userBillingRep;
        private readonly IMediator _mediator;
        public GhestPaymentQueryHandler(IWalletRepository walletRep,
            IWalletTransactionRepository walletTransactionRep,
            IUserRepository userRep,
            IUserBillingRepository userBillingRep,
            IMediator mediator)
        {
            _walletRep = walletRep;
            _walletTransactionRep = walletTransactionRep;
            _userRep = userRep;
            _userBillingRep = userBillingRep;
            _mediator = mediator;
        }
        public async Task<ResponseDto> Handle(GhestPaymentQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto response=new ResponseDto();
            var Ghest =await _walletTransactionRep.GetAsync(request.transactionId);
            var ParentFecilities=await _walletTransactionRep.GetParentFecilities(Ghest.ParentFecilitiesId.Value);

            Ghest.TypeId = 7;
            await _walletTransactionRep.UpdateAsync(Ghest);

            var AllAghsat =await _walletTransactionRep.GetAghsatByParentId(ParentFecilities.Id, 0);
            var TasviyeAndBackToWalletAghsat =await _walletTransactionRep.GetAghsatByParentId(ParentFecilities.Id, 8);
            var TasviyeAghsat =await _walletTransactionRep.GetAghsatByParentId(ParentFecilities.Id, 7);
            var MandeAghsat =await _walletTransactionRep.GetAghsatByParentId(ParentFecilities.Id, 6);

            switch (ParentFecilities.Type)
            {
                case "OrgCredit":
                    var Sum = 0;
                    foreach(var item in TasviyeAghsat)
                    {
                        Sum += item.Price;
                    }
                    if (Sum >= 5000000)
                    {
                        foreach (var item in TasviyeAghsat)
                        {
                            item.TypeId = 8;
                            await _walletTransactionRep.UpdateAsync(item);
                        }
                        WalletTransactionDto transaction = new WalletTransactionDto()
                        {
                            WalletId = ParentFecilities.WalletId,
                            Description = $"شارژ مجدد پس از تسویه اقساط ",
                            Type = "OrgCredit",
                            TypeId = 1,
                            Price = Sum,
                        };
                        var createdTransaction4 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction, true));
                        
                    }


                    //if (AllAghsat.Count() == (TasviyeAghsat.Count()+TasviyeAndBackToWalletAghsat.Count()))
                    //{
                    //    ParentFecilities.Active = false;
                    //    ParentFecilities.Description = $"{ParentFecilities.Description} |تسویه شد";
                    //    await _walletTransactionRep.UpdateAsync(ParentFecilities);
                    //}
                    break;
                case "Fecilities":
                    if (AllAghsat.Count() == TasviyeAghsat.Count())
                    {
                        ParentFecilities.Active = false;
                        ParentFecilities.Description = $"{ParentFecilities.Description} |تسویه شد";
                        await _walletTransactionRep.UpdateAsync(ParentFecilities);
                    }
                    break;
            }


            
            response.IsSuccessed = true;
            return response;
        }
    }
}
