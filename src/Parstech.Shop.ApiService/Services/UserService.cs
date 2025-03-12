using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.User;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class UserService : Parstech.Shop.Shared.Protos.User.UserService.UserServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<User> GetUser(UserRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new UserReadQueryReq(request.UserId));
            return _mapper.Map<User>(user);
        }

        public override async Task<UserPaging> GetUsers(UserPagingRequest request, ServerCallContext context)
        {
            var parameter = _mapper.Map<Shop.Application.DTOs.User.UserParameterDto>(request.Parameter);
            var result = await _mediator.Send(new UserPagingQueryReq(parameter));
            
            var response = new UserPaging
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount
            };

            // Convert the Array to List<UserDto> and then map to proto User objects
            if (result.UserDtos != null)
            {
                foreach (var userDto in result.UserDtos)
                {
                    response.Users.Add(_mapper.Map<User>(userDto));
                }
            }

            return response;
        }

        public override async Task<UserInfo> GetUserInfo(UserInfoRequest request, ServerCallContext context)
        {
            var userInfo = await _mediator.Send(new UserInfoQueryReq(request.UserName));
            return _mapper.Map<UserInfo>(userInfo);
        }

        public override async Task<UserSelectListResponse> GetUsersForSelectList(UserSelectListRequest request, ServerCallContext context)
        {
            var users = await _mediator.Send(new UsersForSelectListQueryReq());
            
            var response = new UserSelectListResponse();
            foreach (var user in users)
            {
                response.Users.Add(_mapper.Map<UserForSelectList>(user));
            }

            return response;
        }

        public override async Task<UserPaging> FilterUsers(UserFilterRequest request, ServerCallContext context)
        {
            var filter = _mapper.Map<Shop.Application.DTOs.User.UserFilterDto>(request.Filter);
            var result = await _mediator.Send(new UserFilterQueryReq(filter));
            
            // Convert result to UserPaging response
            // This part depends on what your filter query returns
            var response = new UserPaging
            {
                CurrentPage = 1,
                PageCount = 1
            };

            // Add users to response if your filter returns a list of users
            // This is a placeholder - adjust according to your actual implementation
            foreach (var user in result)
            {
                response.Users.Add(_mapper.Map<User>(user));
            }

            return response;
        }
    }
}
