using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Categury.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class ShowCateguriesQueryHandler : IRequestHandler<ShowCateguriesQueryReq, List<ParrentCateguryShowDto>>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;
    private readonly ICateguryQueries _categuryQueries;
    private readonly string _connectionString;

    public ShowCateguriesQueryHandler(ICateguryRepository categuryRep,
        IMapper mapper,
        ICateguryQueries categuryQueries,
        IConfiguration configuration)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
        _categuryQueries = categuryQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<ParrentCateguryShowDto>> Handle(ShowCateguriesQueryReq request,
        CancellationToken cancellationToken)
    {
        List<ParrentCateguryShowDto> Result = new();
        //var Parrents =await _categuryRep.GetCateguryByParentId(0,null);
        List<Domain.Models.Categury> Parrents = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuParrentCateguries).ToList());

        foreach (Domain.Models.Categury parrent in Parrents)
        {
            ParrentCateguryShowDto? parrentDto = _mapper.Map<ParrentCateguryShowDto>(parrent);


            List<Domain.Models.Categury> Childs1 = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries,
                        new { @parentId = parrent.GroupId, @row = 1 })
                    .ToList());
            foreach (Domain.Models.Categury subParrent in Childs1)
            {
                SubParrentCateguryShowDto? SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();


                List<Domain.Models.Categury> SubChilds = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                            new { @parentId = subParrent.GroupId })
                        .ToList());
                parrentDto.Childs1.Add(SubParrentDto);
                foreach (Domain.Models.Categury subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    SubCateguryShowDto? SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    List<Domain.Models.Categury> SubChilds2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                                new { @parentId = subp.GroupId })
                            .ToList());


                    foreach (Domain.Models.Categury sub in SubChilds2)
                    {
                        SubCateguryShowDto? Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }

                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }


            List<Domain.Models.Categury> Childs2 = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries,
                        new { @parentId = parrent.GroupId, @row = 2 })
                    .ToList());
            foreach (Domain.Models.Categury subParrent in Childs2)
            {
                SubParrentCateguryShowDto? SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                List<Domain.Models.Categury> SubChilds = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                            new { @parentId = subParrent.GroupId })
                        .ToList());

                parrentDto.Childs2.Add(SubParrentDto);
                foreach (Domain.Models.Categury subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    SubCateguryShowDto? SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    List<Domain.Models.Categury> SubChilds2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                                new { @parentId = subp.GroupId })
                            .ToList());


                    foreach (Domain.Models.Categury sub in SubChilds2)
                    {
                        SubCateguryShowDto? Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }

                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }

            List<Domain.Models.Categury> Childs3 = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries,
                        new { @parentId = parrent.GroupId, @row = 3 })
                    .ToList());
            foreach (Domain.Models.Categury subParrent in Childs3)
            {
                SubParrentCateguryShowDto? SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                List<Domain.Models.Categury> SubChilds = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                            new { @parentId = subParrent.GroupId })
                        .ToList());

                parrentDto.Childs3.Add(SubParrentDto);
                foreach (Domain.Models.Categury subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    SubCateguryShowDto? SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    List<Domain.Models.Categury> SubChilds2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                                new { @parentId = subp.GroupId })
                            .ToList());


                    foreach (Domain.Models.Categury sub in SubChilds2)
                    {
                        SubCateguryShowDto? Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }

                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }


            List<Domain.Models.Categury> Childs4 = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries,
                        new { @parentId = parrent.GroupId, @row = 4 })
                    .ToList());
            foreach (Domain.Models.Categury subParrent in Childs4)
            {
                SubParrentCateguryShowDto? SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                List<Domain.Models.Categury> SubChilds = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                            new { @parentId = subParrent.GroupId })
                        .ToList());

                parrentDto.Childs4.Add(SubParrentDto);
                foreach (Domain.Models.Categury subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    SubCateguryShowDto? SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    List<Domain.Models.Categury> SubChilds2 = DapperHelper.ExecuteCommand(_connectionString,
                        conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries,
                                new { @parentId = subp.GroupId })
                            .ToList());


                    foreach (Domain.Models.Categury sub in SubChilds2)
                    {
                        SubCateguryShowDto? Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }

                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }

            Result.Add(parrentDto);
        }

        return Result;
    }
}