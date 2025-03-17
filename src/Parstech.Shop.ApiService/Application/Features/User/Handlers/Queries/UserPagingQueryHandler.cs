using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class UserPagingQueryHandler : IRequestHandler<UserPagingQueryReq, UserPageingDto>
{
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly string _connectionString;

    public UserPagingQueryHandler(IUserRepository userRep,
        IMapper mapper,
        IUserBillingRepository userBillingRep,
        IConfiguration configuration)
    {
        _userRep = userRep;
        _mapper = mapper;
        _userBillingRep = userBillingRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<UserPageingDto> Handle(UserPagingQueryReq request, CancellationToken cancellationToken)
    {
        int skip = (request.UserParameterDto.CurrentPage - 1) * request.UserParameterDto.TakePage;

        string? query =
            $"SELECT dbo.[User].Id, dbo.[User].UserId, dbo.[User].UserName, dbo.[User].Avatar, dbo.[User].LastLoginDate, dbo.[User].SendSms, dbo.[User].IsDelete, dbo.[User].ActiveCode, dbo.UserBilling.FirstName, dbo.UserBilling.LastName, dbo.UserBilling.EconomicCode FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId ORDER BY dbo.[User].Id Desc OFFSET {skip} ROWS FETCH NEXT {request.UserParameterDto.TakePage} ROWS ONLY";
        List<UserDto>? users =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<UserDto>(query).ToList());


        foreach (UserDto? item in users)
        {
            item.FullName = $"{item.FirstName} {item.LastName}";
        }

        IQueryable<UserDto> result = users.AsQueryable();

        UserPageingDto response = new();

        response.CurrentPage = request.UserParameterDto.CurrentPage;

        int ListCount = _userRep.GetCountOfUsers();
        response.PageCount = ListCount / request.UserParameterDto.TakePage;


        response.UserDtos = result.ToArray();


        if (!string.IsNullOrEmpty(request.UserParameterDto.Filter))
        {
            string Allquery =
                $"SELECT dbo.[User].Id, dbo.[User].UserId, dbo.[User].UserName, dbo.[User].Avatar, dbo.[User].LastLoginDate, dbo.[User].SendSms, dbo.[User].IsDelete, dbo.[User].ActiveCode, dbo.UserBilling.FirstName, dbo.UserBilling.LastName, dbo.UserBilling.EconomicCode FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId";

            List<UserDto> AllList =
                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<UserDto>(Allquery).ToList());
            foreach (UserDto item in AllList)
            {
                item.FullName = $"{item.FirstName} {item.LastName}";
            }

            //var searched = AllList.Where(p =>
            //(p.FullName.Contains(request.UserParameterDto.Filter)) ||
            //    (p.FirstName.Contains(request.UserParameterDto.Filter) ||
            //     (p.LastName.Contains(request.UserParameterDto.Filter)) ||
            //     (p.UserName.Contains(request.UserParameterDto.Filter))

            //     )).ToList();
            List<UserDto> searched = AllList.Where(p => p.Id.ToString() == request.UserParameterDto.Filter).ToList();


            response.CurrentPage = 1;


            response.PageCount = 0;


            response.UserDtos = searched.ToArray();
        }

        return response;
    }
}