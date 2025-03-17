using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Handlers.Queries;

public class PagingFormCreditsQueryHandler : IRequestHandler<PagingFormCreditsQueryReq, List<FormCreditDto>>
{
    private readonly IFormCreditRepository _formCreditRep;
    private readonly IMapper _mapper;
    private string _connectionString;

    public PagingFormCreditsQueryHandler(IFormCreditRepository formCreditRep,
        IMapper mapper,
        IConfiguration configuration
    )
    {
        _formCreditRep = formCreditRep;
        _mapper = mapper;

        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<List<FormCreditDto>> Handle(PagingFormCreditsQueryReq request,
        CancellationToken cancellationToken)
    {
        List<ProductDto> Result = new();
        ParameterDto parameter = request.Parameter;
        List<FormCreditDto>? result = new();

        if (parameter.Filter != null || parameter.FromDate != null || parameter.ToDate != null)
        {
            List<Shared.Models.FormCredit> list = await _formCreditRep.Search(request.Parameter.Filter,
                request.Parameter.FromDate,
                request.Parameter.ToDate);
            result = _mapper.Map<List<FormCreditDto>>(list);
        }
        else
        {
            string query =
                $"Select* from FormCredit ORDER BY CreateDate Desc OFFSET {parameter.Skip} ROWS FETCH NEXT 30 ROWS ONLY ";
            result = DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<FormCreditDto>(query).ToList());
        }

        foreach (FormCreditDto item in result)
        {
            item.CreateDateShmai = item.CreateDate.ToShamsi();
        }

        return result;
    }
}