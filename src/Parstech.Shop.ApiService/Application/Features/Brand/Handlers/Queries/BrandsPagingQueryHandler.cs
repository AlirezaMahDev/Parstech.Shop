using MediatR;

using AutoMapper;

using Dapper;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Handlers.Queries;

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

        string query =
            $"SELECT * FROM dbo.Brand ORDER BY dbo.Brand.BrandId Desc OFFSET {skip} ROWS FETCH NEXT {request.Parameter.TakePage} ROWS ONLY";
        List<BrandDto> brands =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<BrandDto>(query).ToList());


        IQueryable<BrandDto> result = brands.AsQueryable();

        PagingDto response = new();

        response.CurrentPage = request.Parameter.CurrentPage;

        int ListCount = _brandRep.GetCountOfBrands();
        response.PageCount = ListCount / request.Parameter.TakePage;


        response.List = result.ToArray();


        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            string Allquery = $"SELECT * FROM dbo.Brand";

            List<BrandDto> AllList =
                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<BrandDto>(Allquery).ToList());
            foreach (BrandDto item in AllList)
            {
                if (item.LatinBrandTitle == null)
                {
                    item.LatinBrandTitle = "-";
                }
            }

            List<BrandDto> searched = AllList.Where(p =>
                    p.BrandTitle.Contains(request.Parameter.Filter, StringComparison.InvariantCultureIgnoreCase) ||
                    p.LatinBrandTitle.Contains(request.Parameter.Filter, StringComparison.InvariantCultureIgnoreCase))
                .ToList();


            response.CurrentPage = 1;


            response.PageCount = 0;


            response.List = searched.ToArray();
        }

        return response;
    }
}