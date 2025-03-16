using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IAuthAdminGrpcClient
    {
        Task<ResponseDto> LoginOrRegisterRequestAsync(string mobile);
        Task<ResponseDto> LoginByActiveCodeAsync(string mobile, string activeCode);
        Task<ResponseDto> LoginByPasswordAsync(string mobile, string password);
        Task<ResponseDto> RegisterUserAsync(UserRegisterDto userRegister);
    }
} 