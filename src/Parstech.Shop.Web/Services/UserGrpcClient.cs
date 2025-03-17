using Parstech.Shop.Shared.Protos.User;

namespace Parstech.Shop.Web.Services;

public class UserGrpcClient : GrpcClientBase
{
    private readonly UserAuthService.UserAuthServiceClient _authService;
    private readonly UserProfileService.UserProfileServiceClient _profileService;
    private readonly UserProductService.UserProductServiceClient _productService;

    public UserGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _authService = new UserAuthService.UserAuthServiceClient(Channel);
        _profileService = new UserProfileService.UserProfileServiceClient(Channel);
        _productService = new UserProductService.UserProductServiceClient(Channel);
    }

    // Authentication methods
    public async Task<AuthResponse> LoginAsync(string username, string password, bool rememberMe = false)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password,
            RememberMe = rememberMe
        };
        return await _authService.LoginAsync(request);
    }

    public async Task<AuthResponse> RegisterAsync(string email, string username, string password, string firstName, string lastName)
    {
        var request = new RegisterRequest
        {
            Email = email,
            Username = username,
            Password = password,
            FirstName = firstName,
            LastName = lastName
        };
        return await _authService.RegisterAsync(request);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var request = new RefreshTokenRequest
        {
            RefreshToken = refreshToken
        };
        return await _authService.RefreshTokenAsync(request);
    }

    public async Task<ResponseDto> LogoutAsync(string accessToken)
    {
        var request = new LogoutRequest
        {
            AccessToken = accessToken
        };
        return await _authService.LogoutAsync(request);
    }

    public async Task<ResponseDto> ForgotPasswordAsync(string email)
    {
        var request = new ForgotPasswordRequest
        {
            Email = email
        };
        return await _authService.ForgotPasswordAsync(request);
    }

    public async Task<ResponseDto> ResetPasswordAsync(string email, string resetToken, string newPassword)
    {
        var request = new ResetPasswordRequest
        {
            Email = email,
            ResetToken = resetToken,
            NewPassword = newPassword
        };
        return await _authService.ResetPasswordAsync(request);
    }

    public async Task<ResponseDto> ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    {
        var request = new ChangePasswordRequest
        {
            UserId = userId,
            CurrentPassword = currentPassword,
            NewPassword = newPassword
        };
        return await _authService.ChangePasswordAsync(request);
    }

    public async Task<UserResponse> GetCurrentUserAsync(string token)
    {
        var request = new GetCurrentUserRequest
        {
            Token = token
        };
        return await _authService.GetCurrentUserAsync(request);
    }

    // Profile methods
    public async Task<UserProfileResponse> GetUserProfileAsync(long userId)
    {
        var request = new GetUserProfileRequest
        {
            UserId = userId
        };
        return await _profileService.GetUserProfileAsync(request);
    }

    public async Task<UserProfileResponse> UpdateUserProfileAsync(long userId, string firstName, string lastName, 
        string phoneNumber = null, string email = null, string username = null)
    {
        var request = new UpdateUserProfileRequest
        {
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber ?? string.Empty,
            Email = email ?? string.Empty,
            Username = username ?? string.Empty
        };
        return await _profileService.UpdateUserProfileAsync(request);
    }

    public async Task<UserAddressesResponse> GetUserAddressesAsync(long userId)
    {
        var request = new GetUserAddressesRequest
        {
            UserId = userId
        };
        return await _profileService.GetUserAddressesAsync(request);
    }

    public async Task<UserAddressResponse> AddUserAddressAsync(long userId, AddressDto address)
    {
        var request = new AddUserAddressRequest
        {
            UserId = userId,
            Address = address
        };
        return await _profileService.AddUserAddressAsync(request);
    }

    public async Task<UserAddressResponse> UpdateUserAddressAsync(long userId, long addressId, AddressDto address)
    {
        var request = new UpdateUserAddressRequest
        {
            UserId = userId,
            AddressId = addressId,
            Address = address
        };
        return await _profileService.UpdateUserAddressAsync(request);
    }

    public async Task<ResponseDto> DeleteUserAddressAsync(long userId, long addressId)
    {
        var request = new DeleteUserAddressRequest
        {
            UserId = userId,
            AddressId = addressId
        };
        return await _profileService.DeleteUserAddressAsync(request);
    }

    public async Task<ResponseDto> SetDefaultAddressAsync(long userId, long addressId, string addressType)
    {
        var request = new SetDefaultAddressRequest
        {
            UserId = userId,
            AddressId = addressId,
            AddressType = addressType
        };
        return await _profileService.SetDefaultAddressAsync(request);
    }

    public async Task<UserWishlistResponse> GetUserWishlistAsync(long userId)
    {
        var request = new GetUserWishlistRequest
        {
            UserId = userId
        };
        return await _profileService.GetUserWishlistAsync(request);
    }

    public async Task<ResponseDto> AddToWishlistAsync(long userId, long productId)
    {
        var request = new AddToWishlistRequest
        {
            UserId = userId,
            ProductId = productId
        };
        return await _profileService.AddToWishlistAsync(request);
    }

    public async Task<ResponseDto> RemoveFromWishlistAsync(long userId, long productId)
    {
        var request = new RemoveFromWishlistRequest
        {
            UserId = userId,
            ProductId = productId
        };
        return await _profileService.RemoveFromWishlistAsync(request);
    }

    // Product interaction methods
    public async Task<RateProductResponse> RateProductAsync(long userId, long productId, int rating)
    {
        var request = new RateProductRequest
        {
            UserId = userId,
            ProductId = productId,
            Rating = rating
        };
        return await _productService.RateProductAsync(request);
    }

    public async Task<ReviewProductResponse> ReviewProductAsync(long userId, long productId, string title, string content, int rating)
    {
        var request = new ReviewProductRequest
        {
            UserId = userId,
            ProductId = productId,
            Title = title,
            Content = content,
            Rating = rating
        };
        return await _productService.ReviewProductAsync(request);
    }

    public async Task<GetUserReviewsResponse> GetUserReviewsAsync(long userId, int page = 1, int pageSize = 10)
    {
        var request = new GetUserReviewsRequest
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize
        };
        return await _productService.GetUserReviewsAsync(request);
    }

    public async Task<GetProductReviewsResponse> GetProductReviewsAsync(long productId, int page = 1, int pageSize = 10)
    {
        var request = new GetProductReviewsRequest
        {
            ProductId = productId,
            Page = page,
            PageSize = pageSize
        };
        return await _productService.GetProductReviewsAsync(request);
    }
}