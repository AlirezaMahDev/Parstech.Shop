namespace Parstech.Shop.Web.Services.GrpcClients;

public class UserGrpcClient : GrpcClientBase
{
    private readonly UserService.UserServiceClient _client;

    public UserGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new UserService.UserServiceClient(Channel);
    }

    public async Task<User> GetUserAsync(int userId)
    {
        var request = new UserRequest { UserId = userId };
        return await _client.GetUserAsync(request);
    }

    public async Task<UserPaging> GetUsersAsync(UserParameter parameter)
    {
        var request = new UserPagingRequest { Parameter = parameter };
        return await _client.GetUsersAsync(request);
    }

    public async Task<UserInfo> GetUserInfoAsync(string userName)
    {
        var request = new UserInfoRequest { UserName = userName };
        return await _client.GetUserInfoAsync(request);
    }

    public async Task<UserSelectListResponse> GetUsersForSelectListAsync()
    {
        var request = new UserSelectListRequest();
        return await _client.GetUsersForSelectListAsync(request);
    }

    public async Task<UserPaging> FilterUsersAsync(UserFilter filter)
    {
        var request = new UserFilterRequest { Filter = filter };
        return await _client.FilterUsersAsync(request);
    }
}