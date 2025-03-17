using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Queries;

public class WalletTransactionsPagingQueryHandler : IRequestHandler<WalletTransactionsPagingQueryReq, PagingDto>
{
    private readonly IWalletTransactionRepository _walletTransactionRepo;
    private readonly IWalletRepository _walletRep;
    private readonly IMapper _mapper;
    private readonly IWalletTypesRepository _walletTypesRepo;

    public WalletTransactionsPagingQueryHandler(IWalletTransactionRepository walletTransactionRepo,
        IWalletRepository walletRep,
        IMapper mapper,
        IWalletTypesRepository walletTypesRepo)
    {
        _walletTransactionRepo = walletTransactionRepo;
        _walletRep = walletRep;
        _mapper = mapper;
        _walletTypesRepo = walletTypesRepo;
    }

    public async Task<PagingDto> Handle(WalletTransactionsPagingQueryReq request, CancellationToken cancellationToken)
    {
        IList<WalletTransactionDto> walletTransactions = new List<WalletTransactionDto>();
        IReadOnlyList<Domain.Models.WalletTransaction> transactions = await _walletTransactionRepo.GetAll();
        IOrderedEnumerable<Domain.Models.WalletTransaction> transactionsResult = transactions
            .Where(a => a.WalletId == request.Parameter.WalletId && a.Type == request.Parameter.Type)
            .OrderByDescending(u => u.CreateDate);
        foreach (Domain.Models.WalletTransaction item in transactionsResult)
        {
            WalletTransactionDto walletTransactionDto = new();
            walletTransactionDto = _mapper.Map<WalletTransactionDto>(item);
            WalletType? typeOfTransaction = await _walletTypesRepo.GetAsync(item.TypeId);
            walletTransactionDto.TypeName = typeOfTransaction.TypeTitle;
            walletTransactionDto.CreateDateShamsi = walletTransactionDto.CreateDate.ToShamsi();
            if (walletTransactionDto.ExpireDate != null)
            {
                walletTransactionDto.ExpireDateShamsi = walletTransactionDto.ExpireDate.Value.ToShamsi();
            }
            else
            {
                walletTransactionDto.ExpireDateShamsi = "-";
            }

            walletTransactions.Add(walletTransactionDto);
        }

        IQueryable<WalletTransactionDto> result = walletTransactions.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                p.TrackingCode.Contains(request.Parameter.Filter));
        }

        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;
    }
}