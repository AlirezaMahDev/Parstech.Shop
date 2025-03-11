using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.WalletTransaction.Queries;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
    public class GetTransactionByParentIdQueryHandler : IRequestHandler<GetTransactionByParentIdQueryReq, List<WalletTransactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IWalletTransactionRepository _walletTransactionRep;
        
        public GetTransactionByParentIdQueryHandler(IWalletTransactionRepository walletTransactionRep, IMapper mapper)
        {
           
            _walletTransactionRep = walletTransactionRep;
            _mapper = mapper;
            
        }
        public async Task<List<WalletTransactionDto>> Handle(GetTransactionByParentIdQueryReq request, CancellationToken cancellationToken)
        {
            var list =await _walletTransactionRep.GetTransactionsByParentId(request.id);
            var dto=_mapper.Map<List<WalletTransactionDto>>(list);
            foreach (var item in dto)
            {
                item.CreateDateShamsi = item.CreateDate.ToShamsi();
                if (item.ExpireDate != null)
                {
                    item.ExpireDateShamsi = item.ExpireDate.Value.ToShamsi();
                }
               
            }
            return dto;
        }
    }
}
