using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Parstech.Shop.Shared.Protos.UserAdmin;
using Shop.Application.DTOs.IUserRole;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.DTOs.UserShipping;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserBilling.Requests.Commands;
using Shop.Application.Features.UserBilling.Requests.Queries;
using Shop.Application.Features.UserShipping.Requests.Commands;
using Shop.Application.Features.UserShipping.Requests.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace Parstech.Shop.ApiService.Services
{
    public class UserAdminGrpcService : UserAdminService.UserAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserAdminGrpcService> _logger;

        public UserAdminGrpcService(
            IMediator mediator,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<UserAdminGrpcService> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        #region User Operations

        public override async Task<UserPageResponse> GetUsers(UserParameterRequest request, ServerCallContext context)
        {
            var parameter = new UserParameterDto
            {
                PageId = request.PageId,
                Take = request.Take,
                SearchKey = request.SearchKey
            };

            var result = await _mediator.Send(new GetUsersPagingQueryReq(parameter));

            var response = new UserPageResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code,
                TotalRow = result.TotalRow,
                PageId = result.PageId,
                Take = result.Take
            };

            foreach (var user in result.Items)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    WalletCredit = user.WalletCredit,
                    IsActive = user.IsActive,
                    CreateDate = user.CreateDate,
                    LastLoginDate = user.LastLoginDate,
                    NationalCode = user.NationalCode,
                    Address = user.Address,
                    UserAvatar = user.UserAvatar
                };

                foreach (var role in user.Roles)
                {
                    userDto.Roles.Add(role);
                }

                response.Items.Add(userDto);
            }

            return response;
        }

        public override async Task<UserResponse> GetUser(UserIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserDetailQueryReq(request.UserId));

            var response = new UserResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            if (result.Status && result.Data != null)
            {
                var userDto = new UserDto
                {
                    Id = result.Data.Id,
                    UserName = result.Data.UserName,
                    Email = result.Data.Email,
                    PhoneNumber = result.Data.PhoneNumber,
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    WalletCredit = result.Data.WalletCredit,
                    IsActive = result.Data.IsActive,
                    CreateDate = result.Data.CreateDate,
                    LastLoginDate = result.Data.LastLoginDate,
                    NationalCode = result.Data.NationalCode,
                    Address = result.Data.Address,
                    UserAvatar = result.Data.UserAvatar
                };

                foreach (var role in result.Data.Roles)
                {
                    userDto.Roles.Add(role);
                }

                response.User = userDto;
            }

            return response;
        }

        public override async Task<ResponseDto> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var createUserDto = new CreateUserDto
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                NationalCode = request.NationalCode,
                Address = request.Address,
                WalletCredit = request.WalletCredit,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(new CreateUserCommandReq(createUserDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var updateUserDto = new UpdateUserDto
            {
                Id = request.Id,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                NationalCode = request.NationalCode,
                Address = request.Address,
                WalletCredit = request.WalletCredit,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(new UpdateUserCommandReq(updateUserDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<LoginResponse> LoginByUser(UserIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new LoginByUserQueryReq(request.UserId));

            return new LoginResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code,
                Token = result.Data
            };
        }

        #endregion

        #region User Billing Operations

        public override async Task<UserBillingListResponse> GetUserBillings(UserIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserBillingListQueryReq(request.UserId));

            var response = new UserBillingListResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            foreach (var billing in result.Data)
            {
                response.Items.Add(new UserBillingDto
                {
                    Id = billing.Id,
                    UserId = billing.UserId,
                    FirstName = billing.FirstName,
                    LastName = billing.LastName,
                    NationalCode = billing.NationalCode,
                    PhoneNumber = billing.PhoneNumber,
                    Address = billing.Address,
                    PostalCode = billing.PostalCode,
                    Email = billing.Email,
                    Country = billing.Country,
                    City = billing.City,
                    Company = billing.Company,
                    Province = billing.Province
                });
            }

            return response;
        }

        public override async Task<UserBillingResponse> GetUserBilling(BillingIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserBillingDetailQueryReq(request.BillingId));

            var response = new UserBillingResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            if (result.Status && result.Data != null)
            {
                response.Billing = new UserBillingDto
                {
                    Id = result.Data.Id,
                    UserId = result.Data.UserId,
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    NationalCode = result.Data.NationalCode,
                    PhoneNumber = result.Data.PhoneNumber,
                    Address = result.Data.Address,
                    PostalCode = result.Data.PostalCode,
                    Email = result.Data.Email,
                    Country = result.Data.Country,
                    City = result.Data.City,
                    Company = result.Data.Company,
                    Province = result.Data.Province
                };
            }

            return response;
        }

        public override async Task<ResponseDto> CreateUserBilling(CreateUserBillingRequest request, ServerCallContext context)
        {
            var createUserBillingDto = new CreateUserBillingDto
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Email = request.Email,
                Country = request.Country,
                City = request.City,
                Company = request.Company,
                Province = request.Province
            };

            var result = await _mediator.Send(new CreateUserBillingCommandReq(createUserBillingDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> UpdateUserBilling(UpdateUserBillingRequest request, ServerCallContext context)
        {
            var updateUserBillingDto = new UpdateUserBillingDto
            {
                Id = request.Id,
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Email = request.Email,
                Country = request.Country,
                City = request.City,
                Company = request.Company,
                Province = request.Province
            };

            var result = await _mediator.Send(new UpdateUserBillingCommandReq(updateUserBillingDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> DeleteUserBilling(BillingIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteUserBillingCommandReq(request.BillingId));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        #endregion

        #region User Shipping Operations

        public override async Task<UserShippingListResponse> GetUserShippings(UserIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserShippingListQueryReq(request.UserId));

            var response = new UserShippingListResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            foreach (var shipping in result.Data)
            {
                response.Items.Add(new UserShippingDto
                {
                    Id = shipping.Id,
                    UserId = shipping.UserId,
                    FirstName = shipping.FirstName,
                    LastName = shipping.LastName,
                    NationalCode = shipping.NationalCode,
                    PhoneNumber = shipping.PhoneNumber,
                    Address = shipping.Address,
                    PostalCode = shipping.PostalCode,
                    Country = shipping.Country,
                    City = shipping.City,
                    Company = shipping.Company,
                    Province = shipping.Province,
                    IsDefault = shipping.IsDefault
                });
            }

            return response;
        }

        public override async Task<UserShippingResponse> GetUserShipping(ShippingIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetUserShippingDetailQueryReq(request.ShippingId));

            var response = new UserShippingResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            if (result.Status && result.Data != null)
            {
                response.Shipping = new UserShippingDto
                {
                    Id = result.Data.Id,
                    UserId = result.Data.UserId,
                    FirstName = result.Data.FirstName,
                    LastName = result.Data.LastName,
                    NationalCode = result.Data.NationalCode,
                    PhoneNumber = result.Data.PhoneNumber,
                    Address = result.Data.Address,
                    PostalCode = result.Data.PostalCode,
                    Country = result.Data.Country,
                    City = result.Data.City,
                    Company = result.Data.Company,
                    Province = result.Data.Province,
                    IsDefault = result.Data.IsDefault
                };
            }

            return response;
        }

        public override async Task<ResponseDto> CreateUserShipping(CreateUserShippingRequest request, ServerCallContext context)
        {
            var createUserShippingDto = new CreateUserShippingDto
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Country = request.Country,
                City = request.City,
                Company = request.Company,
                Province = request.Province,
                IsDefault = request.IsDefault
            };

            var result = await _mediator.Send(new CreateUserShippingCommandReq(createUserShippingDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> UpdateUserShipping(UpdateUserShippingRequest request, ServerCallContext context)
        {
            var updateUserShippingDto = new UpdateUserShippingDto
            {
                Id = request.Id,
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Country = request.Country,
                City = request.City,
                Company = request.Company,
                Province = request.Province,
                IsDefault = request.IsDefault
            };

            var result = await _mediator.Send(new UpdateUserShippingCommandReq(updateUserShippingDto));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> DeleteUserShipping(ShippingIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteUserShippingCommandReq(request.ShippingId));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        #endregion

        #region User Role Operations

        public override async Task<UserRoleListResponse> GetUserRoles(UserIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetRolesQueryReq(request.UserId));

            var response = new UserRoleListResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };

            foreach (var role in result.Data)
            {
                response.Items.Add(new UserRoleDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    IsSelected = role.IsSelected
                });
            }

            return response;
        }

        public override async Task<ResponseDto> AddUserRole(AddUserRoleRequest request, ServerCallContext context)
        {
            var userRole = new UserRoleDto
            {
                RoleId = request.RoleId
            };

            var result = await _mediator.Send(new AddRoleCommandReq(request.UserId, userRole));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        public override async Task<ResponseDto> DeleteUserRole(DeleteUserRoleRequest request, ServerCallContext context)
        {
            var userRole = new UserRoleDto
            {
                RoleId = request.RoleId
            };

            var result = await _mediator.Send(new DeleteRoleCommandReq(request.UserId, userRole));

            return new ResponseDto
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code
            };
        }

        #endregion
    }
} 