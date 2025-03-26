using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.FormCredit.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Handlers.Queries;

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
            
        _formCreditRep= formCreditRep;
        _mapper = mapper;
           
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
            
    }
       


    public async Task<List<FormCreditDto>> Handle(PagingFormCreditsQueryReq request, CancellationToken cancellationToken)
    {
        var Result = new List<ProductDto>();
        var parameter = request.Parameter;
        var result = new List<FormCreditDto>();
           
        if (parameter.Filter != null || parameter.FromDate != null || parameter.ToDate != null)
        {
            var list = await _formCreditRep.Search(request.Parameter.Filter, request.Parameter.FromDate, request.Parameter.ToDate);
            result=_mapper.Map<List<FormCreditDto>>(list);
        }
        else
        {
            var query = $"Select* from FormCredit ORDER BY CreateDate Desc OFFSET {parameter.Skip} ROWS FETCH NEXT 30 ROWS ONLY ";
            result = DapperHelper.ExecuteCommand< List<FormCreditDto>>(_connectionString, conn => conn.Query<FormCreditDto>(query).ToList());


        }

        foreach( var item in result )
        {
            item.CreateDateShmai = item.CreateDate.ToShamsi();
        }
        return result;
    }
}