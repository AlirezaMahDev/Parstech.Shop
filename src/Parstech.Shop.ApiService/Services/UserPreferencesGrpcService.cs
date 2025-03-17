using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Query;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class UserPreferencesGrpcService : UserPreferencesService.UserPreferencesServiceBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserPreferencesGrpcService(
        IMediator mediator,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _mediator = mediator;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public override async Task<UserBillingResponse> GetUserBilling(UserIdRequest request, ServerCallContext context)
    {
        try
        {
            void billing = await _mediator.Send(new UserBillingOfUserQueryReq(request.UserId));

            return new UserBillingResponse
            {
                Id = billing.Id,
                UserId = billing.UserId,
                CompanyName = billing.CompanyName ?? string.Empty,
                EconomicCode = billing.EconomicCode ?? string.Empty,
                NationalId = billing.NationalId ?? string.Empty,
                RegistrationNumber = billing.RegistrationNumber ?? string.Empty,
                PhoneNumber = billing.PhoneNumber ?? string.Empty,
                PostalCode = billing.PostalCode ?? string.Empty,
                Address = billing.Address ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<UserBillingResponse> UpdateUserBilling(UpdateBillingRequest request,
        ServerCallContext context)
    {
        try
        {
            var billingDto = new Shop.Application.DTOs.UserBilling.UserBillingDto
            {
                Id = request.Id,
                UserId = request.UserId,
                CompanyName = request.CompanyName,
                EconomicCode = request.EconomicCode,
                NationalId = request.NationalId,
                RegistrationNumber = request.RegistrationNumber,
                PhoneNumber = request.PhoneNumber,
                PostalCode = request.PostalCode,
                Address = request.Address
            };

            void updatedBilling = await _mediator.Send(new UserBillingUpdateCommandReq(billingDto));

            return new UserBillingResponse
            {
                Id = updatedBilling.Id,
                UserId = updatedBilling.UserId,
                CompanyName = updatedBilling.CompanyName ?? string.Empty,
                EconomicCode = updatedBilling.EconomicCode ?? string.Empty,
                NationalId = updatedBilling.NationalId ?? string.Empty,
                RegistrationNumber = updatedBilling.RegistrationNumber ?? string.Empty,
                PhoneNumber = updatedBilling.PhoneNumber ?? string.Empty,
                PostalCode = updatedBilling.PostalCode ?? string.Empty,
                Address = updatedBilling.Address ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<StatusResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
    {
        try
        {
            string? userNameClaim = context.GetHttpContext().User.Identity?.Name;
            if (string.IsNullOrEmpty(userNameClaim))
            {
                return new StatusResponse { IsSuccess = false, Message = "User not authenticated" };
            }

            IdentityUser? user = await _userManager.FindByNameAsync(userNameClaim);
            if (user == null)
            {
                return new StatusResponse { IsSuccess = false, Message = "User not found" };
            }

            IdentityResult? changePasswordResult =
                await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                var response = new StatusResponse { IsSuccess = false, Message = "Failed to change password" };

                foreach (IdentityError? error in changePasswordResult.Errors)
                {
                    response.ErrorMessages.Add(error.Description);
                }

                return response;
            }

            await _signInManager.RefreshSignInAsync(user);

            return new StatusResponse { IsSuccess = true, Message = "Password changed successfully" };
        }
        catch (Exception ex)
        {
            return new StatusResponse { IsSuccess = false, Message = $"Error changing password: {ex.Message}" };
        }
    }

    public override async Task<FavoriteProductsResponse> GetFavoriteProducts(UserNameRequest request,
        ServerCallContext context)
    {
        try
        {
            void favorites = await _mediator.Send(new GetFavoriteProductOfUsersQueryReq(request.UserName));

            var response = new FavoriteProductsResponse();

            foreach (var product in favorites)
            {
                response.Products.Add(new ProductItem
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = product.Image ?? string.Empty,
                    Price = product.Price,
                    Discount = product.Discount,
                    BrandName = product.BrandName ?? string.Empty,
                    CategoryName = product.CategoryName ?? string.Empty,
                    IsAvailable = product.IsAvailable,
                    Url = product.Url ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<ComparisonProductsResponse> GetComparisonProducts(UserNameRequest request,
        ServerCallContext context)
    {
        try
        {
            void comparisonProducts = await _mediator.Send(new GetCmparisonProductsOfUsersQueryReq(request.UserName));

            var response = new ComparisonProductsResponse();

            foreach (var product in comparisonProducts)
            {
                response.Products.Add(new ProductItem
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = product.Image ?? string.Empty,
                    Price = product.Price,
                    Discount = product.Discount,
                    BrandName = product.BrandName ?? string.Empty,
                    CategoryName = product.CategoryName ?? string.Empty,
                    IsAvailable = product.IsAvailable,
                    Url = product.Url ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<ShoppingCartResponse> GetShoppingCart(UserIdRequest request, ServerCallContext context)
    {
        try
        {
            void cart = await _mediator.Send(new NotFinallyOrderOfUserQueryReq(request.UserId));

            var response = new ShoppingCartResponse
            {
                OrderId = cart.OrderId,
                UserName = cart.UserName ?? string.Empty,
                Total = cart.Total,
                Discount = cart.Discount,
                FinalPrice = cart.FinalPrice
            };

            foreach (var detail in cart.OrderDetails)
            {
                response.Details.Add(new CartItemDetail
                {
                    Id = detail.Id,
                    OrderId = detail.OrderId,
                    ProductId = detail.ProductId,
                    ProductName = detail.ProductName ?? string.Empty,
                    ProductImage = detail.ProductImage ?? string.Empty,
                    Count = detail.Count,
                    Price = detail.Price,
                    Discount = detail.Discount,
                    Total = detail.Total
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<WalletAmountResponse> GetWalletAmount(UserIdRequest request, ServerCallContext context)
    {
        try
        {
            void wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(request.UserId));

            return new WalletAmountResponse { Amount = wallet.Amount };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<WalletCoinResponse> GetWalletCoin(UserIdRequest request, ServerCallContext context)
    {
        try
        {
            void wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(request.UserId));

            return new WalletCoinResponse { Coin = wallet.Coin };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<WalletFacilitiesResponse> GetWalletFacilities(UserIdRequest request,
        ServerCallContext context)
    {
        try
        {
            void wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(request.UserId));

            return new WalletFacilitiesResponse { Facilities = wallet.Fecilities };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}