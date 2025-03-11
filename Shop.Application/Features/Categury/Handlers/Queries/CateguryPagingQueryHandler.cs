using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Categury.Queries;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Application.Features.Categury.Handlers.Queries
{
    public class CateguryPagingQueryHandler : IRequestHandler<CateguryPagingQueryReq, PagingDto>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;
        private readonly ICateguryQueries _categuryQueries;
        private readonly string _connectionString;

        public CateguryPagingQueryHandler(ICateguryRepository categuryRep,
            IMapper mapper, ICateguryQueries categuryQueries, IConfiguration configuration)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
            _categuryQueries = categuryQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<PagingDto> Handle(CateguryPagingQueryReq request, CancellationToken cancellationToken)
        {
            //var categories = await _categuryRep.GetAll();
            var blankcateguries = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetBlankCateguries).ToList());
            var categuries = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetParrentCateguries).ToList());
            categuries.AddRange(blankcateguries);
            List<CateguryDto> categuryDto = new List<CateguryDto>();

            foreach (var categury in categuries)
            {
                //sath1
                categury.Sath = 1;
                categuryDto.Add(categury);

                var childs = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries, new { @parentId = categury.GroupId }).ToList());
                var childsNotIsParrent = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries, new { @parentId = categury.GroupId }).ToList());
                childs.AddRange(childsNotIsParrent);
                foreach (var child in childs)
                {
                    var parrent = await _categuryRep.GetAsync(child.ParentId.Value);
                    child.ParentTitle = parrent.GroupTitle;
                    //sath2
                    child.Sath = 2;
                    categuryDto.Add(child);

                    var Subchilds = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries, new { @parentId = child.GroupId }).ToList());
                    var SubchildsNotIsParrent = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries, new { @parentId = child.GroupId }).ToList());
                    Subchilds.AddRange(SubchildsNotIsParrent);

                    foreach (var subchild in Subchilds)
                    {
                        var Subparrent = await _categuryRep.GetAsync(child.GroupId);
                        subchild.ParentTitle = Subparrent.GroupTitle;
                        //sath3
                        subchild.Sath = 3;
                        categuryDto.Add(subchild);


                        var Subchilds2 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries, new { @parentId = subchild.GroupId }).ToList());
                        var SubchildsNotIsParrent2 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries, new { @parentId = subchild.GroupId }).ToList());
                        Subchilds2.AddRange(SubchildsNotIsParrent2);
                        
                        foreach (var subchild2 in Subchilds2)
                        {
                            var Subparrent2 = await _categuryRep.GetAsync(subchild.GroupId);
                            subchild2.ParentTitle = Subparrent2.GroupTitle;
                            //sath4
                            subchild2.Sath = 4;
                            categuryDto.Add(subchild2);



                            var Subchilds3 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries, new { @parentId = subchild2.GroupId }).ToList());
                            var SubchildsNotIsParrent3 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries, new { @parentId = subchild2.GroupId }).ToList());
                            Subchilds3.AddRange(SubchildsNotIsParrent3);

                            foreach (var subchild3 in Subchilds3)
                            {
                                var Subparrent3 = await _categuryRep.GetAsync(subchild2.GroupId);
                                subchild3.ParentTitle = Subparrent3.GroupTitle;
                                //sath5
                                subchild3.Sath = 5;
                                categuryDto.Add(subchild3);

                                var Subchilds4 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfParrentCateguries, new { @parentId = subchild3.GroupId }).ToList());
                                var SubchildsNotIsParrent4 = DapperHelper.ExecuteCommand<List<CateguryDto>>(_connectionString, conn => conn.Query<CateguryDto>(_categuryQueries.GetChalidOfChildCateguries, new { @parentId = subchild3.GroupId }).ToList());
                                Subchilds4.AddRange(SubchildsNotIsParrent4);

                                foreach (var subchild4 in Subchilds4)
                                {
                                    var Subparrent4 = await _categuryRep.GetAsync(subchild3.GroupId);
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

            PagingDto response = new PagingDto();

            if (!string.IsNullOrEmpty(request.Parameter.Filter))
            {
                result = result.Where(p =>
                    (p.GroupTitle.Contains(request.Parameter.Filter) ||
                     (p.LatinGroupTitle.Contains(request.Parameter.Filter))));
            }
            int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

            response.CurrentPage = request.Parameter.CurrentPage;
            int count = result.Count();
            response.PageCount = count / request.Parameter.TakePage;


            response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

            return response;
        }
    }
}
