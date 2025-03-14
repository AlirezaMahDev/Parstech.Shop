using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Parstech.Shop.Shared.Protos.UserAuth;
using Shop.Application.Features.Security.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class UserAuthGrpcService : UserAuthService.UserAuthServiceBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public UserAuthGrpcService(
            IMediator mediator,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    request.Username, 
                    request.Password, 
                    request.RememberMe, 
                    lockoutOnFailure: false);
                
                var response = new LoginResponse
                {
                    IsSuccessful = result.Succeeded,
                    IsLockedOut = result.IsLockedOut
                };
                
                if (result.Succeeded)
                {
                    response.Message = "ورود با موفقیت انجام شد . در حال انتقال به پنل";
                    
                    var user = await _userManager.FindByNameAsync(request.Username);
                    var roles = await _userManager.GetRolesAsync(user);
                    
                    if (roles.Contains("Customer"))
                    {
                        response.RedirectUrl = "/Panel";
                    }
                    else
                    {
                        response.RedirectUrl = "/Admin";
                    }
                    
                    var protectedData = await _mediator.Send(new DataProtectQueryReq(request.Username, "protect"));
                    response.ProtectedData = protectedData;
                }
                else
                {
                    response.Message = "کاربری با مشخصات وارد شده یافت نشد";
                    
                    if (result.IsLockedOut)
                    {
                        response.Message = "حساب شما تا تاریخ فلان مسدود شده است.";
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<ProtectDataResponse> ProtectData(ProtectDataRequest request, ServerCallContext context)
        {
            try
            {
                var protectedData = await _mediator.Send(new DataProtectQueryReq(request.Data, request.Purpose));
                return new ProtectDataResponse { ProtectedData = protectedData };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
} 