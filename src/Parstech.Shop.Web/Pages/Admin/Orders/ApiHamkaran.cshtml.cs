using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Rahkaran.Requests.Queries;
using Shop.Application.Features.User.Requests.Commands;
using Parstech.Shop.Shared.Protos.Rahkaran;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Shop.Web.Pages.Admin.Orders
{

    [Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
    public class ApiHamkaranModel : PageModel
    {

        #region Constractor

        private readonly IMediator _mediator;
        private readonly GrpcChannel _channel;
        private readonly RahkaranService.RahkaranServiceClient _rahkaranClient;

        public ApiHamkaranModel(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            
            // Setup gRPC channel
            var apiUrl = configuration["ApiService:Url"];
            var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            _channel = GrpcChannel.ForAddress(apiUrl, new GrpcChannelOptions
            {
                HttpClient = new HttpClient(handler)
            });
            
            _rahkaranClient = new RahkaranService.RahkaranServiceClient(_channel);
        }

        #endregion

        #region Properties

        [BindProperty]
        public int OrderId { get; set; }

        [BindProperty]
        public RahkaranAllDto Response { get; set; } = new RahkaranAllDto();

        [BindProperty]
        public RahkaranOrderDto orderDto { get; set; } = new RahkaranOrderDto();

        [BindProperty]
        public RahkaranUserDto userDto { get; set; } = new RahkaranUserDto();

        [BindProperty]
        public RahkaranProductDto productDto { get; set; } = new RahkaranProductDto();

        [BindProperty]
        public ResponseDto result { get; set; } = new ResponseDto();

        #endregion

        #region Get

        public async Task<IActionResult> OnGet(int id)
        {
            OrderId = id;
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostData(int orderId)
        {
            // Using gRPC client
            var request = new GetRahkaranDataRequest { OrderId = orderId };
            var grpcResponse = await _rahkaranClient.GetAllRahkaranDataAsync(request);
            
            // Map gRPC response to application DTO
            Response = new RahkaranAllDto
            {
                order = grpcResponse.Order != null ? new RahkaranOrderDto
                {
                    OrderId = grpcResponse.Order.OrderId,
                    OrderCode = grpcResponse.Order.OrderCode,
                    RahkaranPishNumber = grpcResponse.Order.RahkaranPishNumber,
                    RahakaranFactorNumber = grpcResponse.Order.RahakaranFactorNumber,
                    RahakaranFactorSerial = grpcResponse.Order.RahakaranFactorSerial
                } : null,
                
                customer = grpcResponse.Customer != null ? new RahkaranUserDto
                {
                    Id = grpcResponse.Customer.Id,
                    UserName = grpcResponse.Customer.UserName,
                    FirstName = grpcResponse.Customer.FirstName,
                    LastName = grpcResponse.Customer.LastName,
                    EconomicCode = grpcResponse.Customer.EconomicCode,
                    NationalCode = grpcResponse.Customer.NationalCode,
                    UserId = grpcResponse.Customer.UserId,
                    RahkaranUserId = grpcResponse.Customer.RahkaranUserId
                } : null,
                
                products = grpcResponse.Products.Select(p => new RahkaranProductDto
                {
                    StockId = p.StockId,
                    DetailId = p.DetailId,
                    Count = p.Count,
                    Price = p.Price,
                    Name = p.Name,
                    Code = p.Code,
                    VariationName = p.VariationName,
                    ProductId = p.ProductId,
                    RahkaranProductId = p.RahkaranProductId,
                    RahkaranUnitId = p.RahkaranUnitId
                }).ToList()
            };

            return new JsonResult(Response);
        }

        #endregion

        #region order
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostOrder(int id)
        {
            // Using gRPC client
            var request = new GetRahkaranOrderRequest { Id = id };
            var response = await _rahkaranClient.GetRahkaranOrderAsync(request);
            
            // Map gRPC response to domain DTO
            var orderDto = response.Order != null ? new RahkaranOrderDto
            {
                OrderId = response.Order.OrderId,
                OrderCode = response.Order.OrderCode,
                RahkaranPishNumber = response.Order.RahkaranPishNumber,
                RahakaranFactorNumber = response.Order.RahakaranFactorNumber,
                RahakaranFactorSerial = response.Order.RahakaranFactorSerial
            } : null;

            return new JsonResult(orderDto);
        }

        public async Task<IActionResult> OnPostAEOrder(int type)
        {
            if (type == 1)
            {
                // Create order using gRPC
                var request = new CreateRahkaranOrderRequest
                {
                    Order = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                    {
                        OrderId = orderDto.OrderId,
                        OrderCode = orderDto.OrderCode,
                        RahkaranPishNumber = orderDto.RahkaranPishNumber ?? string.Empty,
                        RahakaranFactorNumber = orderDto.RahakaranFactorNumber ?? string.Empty,
                        RahakaranFactorSerial = orderDto.RahakaranFactorSerial ?? string.Empty
                    }
                };
                
                var response = await _rahkaranClient.CreateRahkaranOrderAsync(request);
                
                // Map response back to domain DTO
                var result = response.Order != null ? new RahkaranOrderDto
                {
                    OrderId = response.Order.OrderId,
                    OrderCode = response.Order.OrderCode,
                    RahkaranPishNumber = response.Order.RahkaranPishNumber,
                    RahakaranFactorNumber = response.Order.RahakaranFactorNumber,
                    RahakaranFactorSerial = response.Order.RahakaranFactorSerial
                } : null;

                return new JsonResult(result);
            }
            else
            {
                // Update order using gRPC
                var request = new UpdateRahkaranOrderRequest
                {
                    Order = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                    {
                        OrderId = orderDto.OrderId,
                        OrderCode = orderDto.OrderCode,
                        RahkaranPishNumber = orderDto.RahkaranPishNumber ?? string.Empty,
                        RahakaranFactorNumber = orderDto.RahakaranFactorNumber ?? string.Empty,
                        RahakaranFactorSerial = orderDto.RahakaranFactorSerial ?? string.Empty
                    }
                };
                
                var response = await _rahkaranClient.UpdateRahkaranOrderAsync(request);
                
                // Map response back to domain DTO
                var result = response.Order != null ? new RahkaranOrderDto
                {
                    OrderId = response.Order.OrderId,
                    OrderCode = response.Order.OrderCode,
                    RahkaranPishNumber = response.Order.RahkaranPishNumber,
                    RahakaranFactorNumber = response.Order.RahakaranFactorNumber,
                    RahakaranFactorSerial = response.Order.RahakaranFactorSerial
                } : null;

                return new JsonResult(result);
            }
        }
        #endregion

        #region user
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUser(int id)
        {
            // Using gRPC client
            var request = new GetRahkaranUserRequest { Id = id };
            var response = await _rahkaranClient.GetRahkaranUserAsync(request);
            
            // Map gRPC response to domain DTO
            var userDto = response.User != null ? new RahkaranUserDto
            {
                Id = response.User.Id,
                UserName = response.User.UserName,
                FirstName = response.User.FirstName,
                LastName = response.User.LastName,
                EconomicCode = response.User.EconomicCode,
                NationalCode = response.User.NationalCode,
                UserId = response.User.UserId,
                RahkaranUserId = response.User.RahkaranUserId
            } : null;

            return new JsonResult(userDto);
        }

        public async Task<IActionResult> OnPostAEUser(int type)
        {
            if (type == 1)
            {
                // Create user using gRPC
                var request = new CreateRahkaranUserRequest
                {
                    User = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                    {
                        Id = userDto.Id,
                        UserName = userDto.UserName,
                        FirstName = userDto.FirstName ?? string.Empty,
                        LastName = userDto.LastName ?? string.Empty,
                        EconomicCode = userDto.EconomicCode ?? string.Empty,
                        NationalCode = userDto.NationalCode ?? string.Empty,
                        UserId = userDto.UserId ?? 0,
                        RahkaranUserId = userDto.RahkaranUserId ?? string.Empty
                    }
                };
                
                var response = await _rahkaranClient.CreateRahkaranUserAsync(request);
                
                // Map response back to domain DTO
                var result = response.User != null ? new RahkaranUserDto
                {
                    Id = response.User.Id,
                    UserName = response.User.UserName,
                    FirstName = response.User.FirstName,
                    LastName = response.User.LastName,
                    EconomicCode = response.User.EconomicCode,
                    NationalCode = response.User.NationalCode,
                    UserId = response.User.UserId,
                    RahkaranUserId = response.User.RahkaranUserId
                } : null;

                return new JsonResult(result);
            }
            else
            {
                // Update user using gRPC
                var request = new UpdateRahkaranUserRequest
                {
                    User = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                    {
                        Id = userDto.Id,
                        UserName = userDto.UserName,
                        FirstName = userDto.FirstName ?? string.Empty,
                        LastName = userDto.LastName ?? string.Empty,
                        EconomicCode = userDto.EconomicCode ?? string.Empty,
                        NationalCode = userDto.NationalCode ?? string.Empty,
                        UserId = userDto.UserId ?? 0,
                        RahkaranUserId = userDto.RahkaranUserId ?? string.Empty
                    }
                };
                
                var response = await _rahkaranClient.UpdateRahkaranUserAsync(request);
                
                // Map response back to domain DTO
                var result = response.User != null ? new RahkaranUserDto
                {
                    Id = response.User.Id,
                    UserName = response.User.UserName,
                    FirstName = response.User.FirstName,
                    LastName = response.User.LastName,
                    EconomicCode = response.User.EconomicCode,
                    NationalCode = response.User.NationalCode,
                    UserId = response.User.UserId,
                    RahkaranUserId = response.User.RahkaranUserId
                } : null;

                return new JsonResult(result);
            }
        }
        #endregion

        #region product
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostProduct(int id)
        {
            // Using gRPC client
            var request = new GetRahkaranProductRequest { Id = id };
            var response = await _rahkaranClient.GetRahkaranProductAsync(request);
            
            // Map gRPC response to domain DTO
            var productDto = response.Product != null ? new RahkaranProductDto
            {
                StockId = response.Product.StockId,
                DetailId = response.Product.DetailId,
                Count = response.Product.Count,
                Price = response.Product.Price,
                Name = response.Product.Name,
                Code = response.Product.Code,
                VariationName = response.Product.VariationName,
                ProductId = response.Product.ProductId,
                RahkaranProductId = response.Product.RahkaranProductId,
                RahkaranUnitId = response.Product.RahkaranUnitId
            } : null;

            return new JsonResult(productDto);
        }

        public async Task<IActionResult> OnPostAEProduct(int type)
        {
            if (type == 1)
            {
                // Create product using gRPC
                var request = new CreateRahkaranProductRequest
                {
                    Product = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                    {
                        StockId = productDto.StockId ?? 0,
                        DetailId = productDto.DetailId ?? 0,
                        Count = productDto.Count ?? 0,
                        Price = productDto.Price ?? 0,
                        Name = productDto.Name,
                        Code = productDto.Code ?? string.Empty,
                        VariationName = productDto.VariationName ?? string.Empty,
                        ProductId = productDto.ProductId ?? 0,
                        RahkaranProductId = productDto.RahkaranProductId ?? string.Empty,
                        RahkaranUnitId = productDto.RahkaranUnitId ?? 0
                    }
                };
                
                var response = await _rahkaranClient.CreateRahkaranProductAsync(request);
                
                // Map response back to domain DTO
                var result = response.Product != null ? new RahkaranProductDto
                {
                    StockId = response.Product.StockId,
                    DetailId = response.Product.DetailId,
                    Count = response.Product.Count,
                    Price = response.Product.Price,
                    Name = response.Product.Name,
                    Code = response.Product.Code,
                    VariationName = response.Product.VariationName,
                    ProductId = response.Product.ProductId,
                    RahkaranProductId = response.Product.RahkaranProductId,
                    RahkaranUnitId = response.Product.RahkaranUnitId
                } : null;

                return new JsonResult(result);
            }
            else
            {
                // Update product using gRPC
                var request = new UpdateRahkaranProductRequest
                {
                    Product = new Parstech.Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                    {
                        StockId = productDto.StockId ?? 0,
                        DetailId = productDto.DetailId ?? 0,
                        Count = productDto.Count ?? 0,
                        Price = productDto.Price ?? 0,
                        Name = productDto.Name,
                        Code = productDto.Code ?? string.Empty,
                        VariationName = productDto.VariationName ?? string.Empty,
                        ProductId = productDto.ProductId ?? 0,
                        RahkaranProductId = productDto.RahkaranProductId ?? string.Empty,
                        RahkaranUnitId = productDto.RahkaranUnitId ?? 0
                    }
                };
                
                var response = await _rahkaranClient.UpdateRahkaranProductAsync(request);
                
                // Map response back to domain DTO
                var result = response.Product != null ? new RahkaranProductDto
                {
                    StockId = response.Product.StockId,
                    DetailId = response.Product.DetailId,
                    Count = response.Product.Count,
                    Price = response.Product.Price,
                    Name = response.Product.Name,
                    Code = response.Product.Code,
                    VariationName = response.Product.VariationName,
                    ProductId = response.Product.ProductId,
                    RahkaranProductId = response.Product.RahkaranProductId,
                    RahkaranUnitId = response.Product.RahkaranUnitId
                } : null;

                return new JsonResult(result);
            }
        }
        #endregion

        #region Send Api
        public async Task<IActionResult> OnPostSendOrder(int orderId)
        {
            // Using gRPC client
            var request = new SendOrderToApiRequest { OrderId = orderId };
            var response = await _rahkaranClient.SendOrderToApiAsync(request);
            
            // Map gRPC response to domain DTO
            result = new ResponseDto
            {
                IsSuccessed = response.IsSuccess,
                Message = response.Message
            };
            
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostFollowOrder(int orderId)
        {
            // Using gRPC client
            var request = new FollowOrderFromApiRequest { OrderId = orderId };
            var response = await _rahkaranClient.FollowOrderFromApiAsync(request);
            
            // Map gRPC response to domain DTO
            result = new ResponseDto
            {
                IsSuccessed = response.IsSuccess,
                Message = response.Message
            };
            
            return new JsonResult(result);
        }
        #endregion

    }
}
