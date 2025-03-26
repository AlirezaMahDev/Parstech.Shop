using AutoMapper;
using Dapper;
using MediatR;

using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Categury.Queries;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

public class ShowCateguriesQueryHandler : IRequestHandler<ShowCateguriesQueryReq, List<ParrentCateguryShowDto>>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;
    private readonly ICateguryQueries _categuryQueries;
    private readonly string _connectionString;
    public ShowCateguriesQueryHandler(ICateguryRepository categuryRep, IMapper mapper,
        ICateguryQueries categuryQueries, IConfiguration configuration)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
        _categuryQueries = categuryQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<ParrentCateguryShowDto>> Handle(ShowCateguriesQueryReq request, CancellationToken cancellationToken)
    {
            
        List<ParrentCateguryShowDto> Result = new();
        //var Parrents =await _categuryRep.GetCateguryByParentId(0,null);
        var Parrents = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuParrentCateguries).ToList());

        foreach (var parrent in Parrents)
        {
            var parrentDto = _mapper.Map<ParrentCateguryShowDto>(parrent);

                
            var Childs1 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries, new { @parentId = parrent.GroupId , @row =1}).ToList());
            foreach (var subParrent in Childs1)
            {
                var SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                    
                var SubChilds = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subParrent.GroupId }).ToList());
                parrentDto.Childs1.Add(SubParrentDto);
                foreach (var subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    var SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();
                        

                    var SubChilds2 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subp.GroupId }).ToList());

                        
                    foreach (var sub in SubChilds2)
                    {
                        var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }
                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
                    
            }


            var Childs2 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries, new { @parentId = parrent.GroupId, @row = 2 }).ToList());
            foreach (var subParrent in Childs2)
            {
                var SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                var SubChilds = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subParrent.GroupId }).ToList());

                parrentDto.Childs2.Add(SubParrentDto);
                foreach (var subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    var SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    var SubChilds2 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subp.GroupId }).ToList());


                    foreach (var sub in SubChilds2)
                    {
                        var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }
                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }

            var Childs3 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries, new { @parentId = parrent.GroupId, @row = 3 }).ToList());
            foreach (var subParrent in Childs3)
            {
                var SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                var SubChilds = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subParrent.GroupId }).ToList());

                parrentDto.Childs3.Add(SubParrentDto);
                foreach (var subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    var SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    var SubChilds2 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subp.GroupId }).ToList());


                    foreach (var sub in SubChilds2)
                    {
                        var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                        SubParrentDto2.Childs.Add(Sub);
                    }
                    SubParrentDto.Childs.Add(SubParrentDto2);
                }
            }


            var Childs4 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfParrentCateguries, new { @parentId = parrent.GroupId, @row = 4 }).ToList());
            foreach (var subParrent in Childs4)
            {
                var SubParrentDto = _mapper.Map<SubParrentCateguryShowDto>(subParrent);
                SubParrentDto.Childs = new();

                //var SubChilds = await _categuryRep.GetCateguryByParentId(subParrent.GroupId,null);
                var SubChilds = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subParrent.GroupId }).ToList());

                parrentDto.Childs4.Add(SubParrentDto);
                foreach (var subp in SubChilds)
                {
                    //var Sub = _mapper.Map<SubCateguryShowDto>(sub);
                    //SubParrentDto.Childs.Add(Sub);

                    var SubParrentDto2 = _mapper.Map<SubCateguryShowDto>(subp);
                    SubParrentDto2.Childs = new();


                    var SubChilds2 = DapperHelper.ExecuteCommand<List<Domain.Models.Categury>>(_connectionString, conn => conn.Query<Domain.Models.Categury>(_categuryQueries.GetMenuChalidOfChildCateguries, new { @parentId = subp.GroupId }).ToList());


                    foreach (var sub in SubChilds2)
                    {
                        var Sub = _mapper.Map<SubCateguryShowDto>(sub);
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