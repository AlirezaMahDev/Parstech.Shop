using AutoMapper;

using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Brand.Handlers.Queries;

public class BrandsPagingQueryHandler : IRequestHandler<BrandsPagingQueryReq, PagingDto>
{
    private readonly IBrandRepository _brandRep;
    private readonly IMapper _mapper;
    private readonly string _connectionString;

    public BrandsPagingQueryHandler(IBrandRepository brandRep,
        IMapper mapper,
        IConfiguration configuration)
    {
        _brandRep = brandRep;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<PagingDto> Handle(BrandsPagingQueryReq request, CancellationToken cancellationToken)
    {
            
        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        var query = $"SELECT * FROM dbo.Brand ORDER BY dbo.Brand.BrandId Desc OFFSET {skip} ROWS FETCH NEXT {request.Parameter.TakePage} ROWS ONLY";
        var brands = DapperHelper.ExecuteCommand<List<BrandDto>>(_connectionString, conn => conn.Query<BrandDto>(query).ToList());


            

        IQueryable<BrandDto> result = brands.AsQueryable();

        PagingDto response = new();

        response.CurrentPage = request.Parameter.CurrentPage;

        var ListCount = _brandRep.GetCountOfBrands();
        response.PageCount = ListCount / request.Parameter.TakePage;


        response.List = result.ToArray();


        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            var Allquery = $"SELECT * FROM dbo.Brand";

            var AllList = DapperHelper.ExecuteCommand<List<BrandDto>>(_connectionString, conn => conn.Query<BrandDto>(Allquery).ToList());
            foreach(var item in AllList )
            {
                if (item.LatinBrandTitle == null)
                {
                    item.LatinBrandTitle = "-";
                }
            }
            var searched = AllList.Where(p =>
                    (p.BrandTitle.Contains(request.Parameter.Filter, StringComparison.InvariantCultureIgnoreCase)) ||
                    (p.LatinBrandTitle.Contains(request.Parameter.Filter, StringComparison.InvariantCultureIgnoreCase))) 
                .ToList();


            response.CurrentPage = 1;


            response.PageCount = 0;


            response.List = searched.ToArray();


        }

        return response;

    }
}