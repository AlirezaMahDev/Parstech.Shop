using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class UserRegisterQueryHandler : IRequestHandler<UserRegisterQueryReq, ResponseDto>
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserRepository _userRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserRegisterQueryHandler(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        IUserRepository userRep,
        IUserBillingRepository userBillingRep,
        IMediator mediator,
        IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userStore = userStore;
        _userRep = userRep;
        _userBillingRep = userBillingRep;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Handle(UserRegisterQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        if (request.UserRegisterDto.Password == null)
        {
            request.UserRegisterDto.Password = "12345678";
        }

        IdentityUser user = Activator.CreateInstance<IdentityUser>();
        await _userStore.SetUserNameAsync(user, request.UserRegisterDto.UserName, CancellationToken.None);
        IdentityResult result = await _userManager.CreateAsync(user, request.UserRegisterDto.Password);
        if (result.Succeeded)
        {
            string userId = await _userManager.GetUserIdAsync(user);
            //request.UserRegisterDto.IUserId = userId;

            UserDto? userDto = _mapper.Map<UserDto>(request.UserRegisterDto);
            userDto.UserId = userId;
            userDto.IsDelete = false;
            userDto.LastLoginDate = DateTime.Now;
            var mainUser = await _mediator.Send(new UserCreateCommandReq(userDto));


            request.UserRegisterDto.NumberuserId = mainUser.Id;

            if (request.UserRegisterDto.RoleName == null)
            {
                request.UserRegisterDto.RoleName = "Customer";
            }

            IUserRoleDto? userRole = _mapper.Map<IUserRoleDto>(request.UserRegisterDto);
            userRole.UserId = request.UserRegisterDto.IUserId;
            await _mediator.Send(new UserRoleCreateQueryReq(userRole));

            request.UserRegisterDto.UserId = mainUser.Id;
            UserBillingDto? userBillingDto = _mapper.Map<UserBillingDto>(request.UserRegisterDto);
            await _mediator.Send(new UserBillingCreateCommandReq(userBillingDto));

            //credit
            await _mediator.Send(new GetCreditOfNationalCodeQueryReq(mainUser.Id, userBillingDto.NationalCode));
            Response.IsSuccessed = true;
            Response.Message = "ثبت نام شما با موفقیت انجام شد. جهت ورود به پنل کاربری با شماره همراه اقدام فرمایید . ";
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "امکان ثبت نام برای شماره همراه وارد شده وجود ندارد.با شماره همراه دیگری اقدام فرمایید";
        }

        return Response;
    }
}