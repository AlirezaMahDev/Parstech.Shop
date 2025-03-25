using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

using ResponseDto = Parstech.Shop.Shared.DTOs.ResponseDto;

namespace Parstech.Shop.ApiService.Services;

public class AuthAdminGrpcService : AuthAdminService.AuthAdminServiceBase
{
    private readonly IMediator _mediator;

    public AuthAdminGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<ResponseDto> LoginOrRegisterRequest(LoginMobileRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new LoginOrRegisterRequestQueryReq(request.Mobile));

            return new()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message ?? string.Empty,
                Token = result.Token ?? string.Empty,
                RefreshToken = result.RefreshToken ?? string.Empty,
                NeedRegister = result.NeedRegister,
                NeedActiveCode = result.NeedActiveCode,
                NeedPassword = result.NeedPassword,
                NeedResendActiveCode = result.NeedResendActiveCode,
                ActiveRegister = result.ActiveRegister ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> LoginByActiveCode(LoginActiveCodeRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new LoginByActiveCodeQueryReq(request.Mobile, request.ActiveCode));

            return new()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message ?? string.Empty,
                Token = result.Token ?? string.Empty,
                RefreshToken = result.RefreshToken ?? string.Empty,
                NeedRegister = result.NeedRegister,
                NeedActiveCode = result.NeedActiveCode,
                NeedPassword = result.NeedPassword,
                NeedResendActiveCode = result.NeedResendActiveCode,
                ActiveRegister = result.ActiveRegister ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> LoginByPassword(LoginPasswordRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new LoginByPasswordQueryReq(request.Mobile, request.Password));

            return new()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message ?? string.Empty,
                Token = result.Token ?? string.Empty,
                RefreshToken = result.RefreshToken ?? string.Empty,
                NeedRegister = result.NeedRegister,
                NeedActiveCode = result.NeedActiveCode,
                NeedPassword = result.NeedPassword,
                NeedResendActiveCode = result.NeedResendActiveCode,
                ActiveRegister = result.ActiveRegister ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> RegisterUser(RegisterUserRequest request, ServerCallContext context)
    {
        try
        {
            var userRegisterDto = new Shop.Application.DTOs.User.UserRegisterDto
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                Country = request.Country,
                State = request.State,
                City = request.City,
                Address = request.Address,
                Mobile = request.Mobile
            };

            var result = await _mediator.Send(new UserRegisterQueryReq(userRegisterDto));

            return new()
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message ?? string.Empty,
                Token = result.Token ?? string.Empty,
                RefreshToken = result.RefreshToken ?? string.Empty,
                NeedRegister = result.NeedRegister,
                NeedActiveCode = result.NeedActiveCode,
                NeedPassword = result.NeedPassword,
                NeedResendActiveCode = result.NeedResendActiveCode,
                ActiveRegister = result.ActiveRegister ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }
}