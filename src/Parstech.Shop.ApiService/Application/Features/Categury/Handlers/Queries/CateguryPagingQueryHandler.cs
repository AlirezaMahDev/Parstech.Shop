using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Categury.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class CateguryPagingQueryHandler : IRequestHandler<CateguryPagingQueryReq, PagingDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;
    private readonly ICateguryQueries _categuryQueries;
    private readonly string _connectionString;

    public CateguryPagingQueryHandler(ICateguryRepository categuryRep,
        IMapper mapper,
        ICateguryQueries categuryQueries,
        IConfiguration configuration)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
        _categuryQueries = categuryQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<PagingDto> Handle(CateguryPagingQueryReq request, CancellationToken cancellationToken)
    {
        //var categories = await _categuryRep.GetAll();
        List<CateguryDto>? blankcateguries = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<CateguryDto>(_categuryQueries.GetBlankCateguries).ToList());
        List<CateguryDto>? categuries = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<CateguryDto>(_categuryQueries.GetParrentCateguries).ToList());
        categuries.AddRange(blankcateguries);
        List<CateguryDto> categuryDto = new();

        foreach (CateguryDto categury in categuries)
        {
            //sath1
            categury.Sath = 1;
            categuryDto.Add(categury);

            List<CateguryDto> childs = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries,
                        new { @parentId = categury.GroupId })
                    .ToList());
            List<CateguryDto> childsNotIsParrent = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries,
                        new { @parentId = categury.GroupId })
                    .ToList());
            childs.AddRange(childsNotIsParrent);
            foreach (CateguryDto child in childs)
            {
                Domain.Models.Categury? parrent = await _categuryRep.GetAsync(child.ParentId.Value);
                child.ParentTitle = parrent.GroupTitle;
                //sath2
                child.Sath = 2;
                categuryDto.Add(child);

                List<CateguryDto> Subchilds = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries,
                            new { @parentId = child.GroupId })
                        .ToList());
                List<CateguryDto> SubchildsNotIsParrent = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries,
                            new { @parentId = child.GroupId })
                        .ToList());
                Subchilds.AddRange(SubchildsNotIsParrent);

                foreach (CateguryDto subchild in Subchilds)
                {
                    Domain.Models.Categury? Subparrent = await _categuryRep.GetAsync(child.GroupId);
                    subchild.ParentTitle = Subparrent.GroupTitle;
                    //sath3
                    subchild.Sath = 3;
                    categuryDto.Add(subchild);


                    List<CateguryDto> Subchilds2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries,
                                new { @parentId = subchild.GroupId })
                            .ToList());
                    List<CateguryDto> SubchildsNotIsParrent2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries,
                                new { @parentId = subchild.GroupId })
                            .ToList());
                    Subchilds2.AddRange(SubchildsNotIsParrent2);

                    foreach (CateguryDto subchild2 in Subchilds2)
                    {
                        Domain.Models.Categury? Subparrent2 = await _categuryRep.GetAsync(subchild.GroupId);
                        subchild2.ParentTitle = Subparrent2.GroupTitle;
                        //sath4
                        subchild2.Sath = 4;
                        categuryDto.Add(subchild2);


                        List<CateguryDto> Subchilds3 = DapperHelper.ExecuteCommand(_connectionString,
                            conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries,
                                    new { @parentId = subchild2.GroupId })
                                .ToList());
                        List<CateguryDto> SubchildsNotIsParrent3 = DapperHelper.ExecuteCommand(_connectionString,
                            conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries,
                                    new { @parentId = subchild2.GroupId })
                                .ToList());
                        Subchilds3.AddRange(SubchildsNotIsParrent3);

                        foreach (CateguryDto subchild3 in Subchilds3)
                        {
                            Domain.Models.Categury? Subparrent3 = await _categuryRep.GetAsync(subchild2.GroupId);
                            subchild3.ParentTitle = Subparrent3.GroupTitle;
                            //sath5
                            subchild3.Sath = 5;
                            categuryDto.Add(subchild3);

                            List<CateguryDto> Subchilds4 = DapperHelper.ExecuteCommand(_connectionString,
                                conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries,
                                        new { @parentId = subchild3.GroupId })
                                    .ToList());
                            List<CateguryDto> SubchildsNotIsParrent4 = DapperHelper.ExecuteCommand(_connectionString,
                                conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries,
                                        new { @parentId = subchild3.GroupId })
                                    .ToList());
                            Subchilds4.AddRange(SubchildsNotIsParrent4);

                            foreach (CateguryDto subchild4 in Subchilds4)
                            {
                                Domain.Models.Categury? Subparrent4 = await _categuryRep.GetAsync(subchild3.GroupId);
                                subchild4.ParentTitle = Subparrent4.GroupTitle;
                                //sath6
                                subchild4.Sath = 6;
                                categuryDto.Add(subchild4);
                            }
                        }
                    }
                }
            }
        }

        IQueryable<CateguryDto> result = categuryDto.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                p.GroupTitle.Contains(request.Parameter.Filter) ||
                p.LatinGroupTitle.Contains(request.Parameter.Filter));
        }

        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;
    }
}