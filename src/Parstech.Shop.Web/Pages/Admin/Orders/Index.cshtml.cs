using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.PayType;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Status;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.OrderPay.Request.Command;
using Shop.Application.Features.OrderPay.Request.Queries;
using Shop.Application.Features.OrderShipping.Request.Queries;
using Shop.Application.Features.OrderStatus.Requests.Commands;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Shared.Protos.Order;
using Parstech.Shop.Web.Services.GrpcClients;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Linq;

namespace Shop.Web.Pages.Admin.Orders
{
    [Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
    public class IndexModel : PageModel
    {

        #region Constractor

        private readonly IMediator _mediator;
        private readonly OrderGrpcClient _orderGrpcClient;

        public IndexModel(IMediator mediator, OrderGrpcClient orderGrpcClient)
        {
            _mediator = mediator;
            _orderGrpcClient = orderGrpcClient;
        }

        #endregion

        #region Properties

        [BindProperty]
        public ParameterDto Parameter { get; set; } = new ParameterDto();

        [BindProperty]
        public PagingDto List { get; set; }

        [BindProperty]
        public OrderDto OrderDto { get; set; }

        [BindProperty]
        public List<OrderDto> Orders { get; set; }

        [BindProperty]
        public int OrderId { get; set; }

        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();
        [BindProperty]
        public OrderDetailShowDto OrderDetailShowDto { get; set; }
        [BindProperty]
        public List<StatusDto> Statuses { get; set; }

        [BindProperty]
        public List<PayTypeDto> payTypes { get; set; }
        [BindProperty]
        public OrderStatusDto OrderStatusDto { get; set; }
        [BindProperty]
        public OrderShippingDto OrderShippingDto { get; set; }
        [BindProperty]
        public OrderShippingChangeDto orderShippingChangeDto { get; set; }
        [BindProperty]
        public int UserShippingId { get; set; }

        [BindProperty]
        public OrderPayDto orderPayDto { get; set; }


        [BindProperty]
        public OrderFilterDto orderFilterDto { get; set; }




        #endregion

        #region Get

        public async Task<IActionResult> OnGet()

        {
            string storeName = null;
            if (User.IsInRole("Store"))
            {
                storeName = User.Identity.Name;
            }

            // Use gRPC client
            var filters = await _orderGrpcClient.GetOrderFiltersAsync(storeName);
            
            // Map gRPC response to application DTOs
            orderFilterDto = MapFromOrderFilterGrpc(filters);

            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            Parameter.TakePage = 30;
            if (User.IsInRole("Store"))
            {
                Parameter.store = User.Identity.Name;
            }
            else
            {
                Parameter.store = null;
            }

            // Use gRPC client
            var parameterGrpc = MapToParameterGrpc(Parameter);
            var pagingResult = await _orderGrpcClient.GetOrdersPagingAsync(parameterGrpc);
            
            // Map gRPC response to application DTOs
            List = MapFromPagingDtoGrpc(pagingResult);
            
            Response.Object = List;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        #endregion
        #region Search Paging
        public async Task<IActionResult> OnPostSearch()
        {
            Parameter.CurrentPage = 1;
            Parameter.TakePage = 30;
            if (User.IsInRole("Store"))
            {
                Parameter.store = User.Identity.Name;
            }
            else
            {
                Parameter.store = null;
            }
            
            // Use gRPC client
            var parameterGrpc = MapToParameterGrpc(Parameter);
            var pagingResult = await _orderGrpcClient.GetOrdersPagingAsync(parameterGrpc);
            
            // Map gRPC response to application DTOs
            List = MapFromPagingDtoGrpc(pagingResult);
            
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            if (User.IsInRole("Store"))
            {
                Parameter.store = User.Identity.Name;
            }
            else
            {
                Parameter.store = null;
            }
            
            // Use gRPC client
            var parameterGrpc = MapToParameterGrpc(Parameter);
            var pagingResult = await _orderGrpcClient.GetOrdersPagingAsync(parameterGrpc);
            
            // Map gRPC response to application DTOs
            List = MapFromPagingDtoGrpc(pagingResult);
            
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }


        #endregion
        #region Show Order Detail

        public async Task<IActionResult> OnPostShowOrderDetail()
        {
            // Use gRPC client
            var orderDetail = await _orderGrpcClient.GetOrderDetailsAsync(OrderId);
            
            // Map gRPC response to application DTOs
            OrderDetailShowDto = MapFromOrderDetailShowGrpc(orderDetail);
            
            Response.Object = OrderDetailShowDto;
            return new JsonResult(Response);
        }

        #endregion
        #region Status Change

        public async Task<IActionResult> OnPostStatusChange(IFormFile file)
        {
            OrderStatusDto.CreateDate = DateTime.Now;
            OrderStatusDto.CreateBy = User.Identity.Name;
            OrderStatusDto.File = file;
            
            // Use gRPC client
            var orderStatusGrpc = new Parstech.Shop.Shared.Protos.Order.OrderStatusDto
            {
                Id = OrderStatusDto.Id,
                OrderId = OrderStatusDto.OrderId,
                StatusId = OrderStatusDto.StatusId,
                Description = OrderStatusDto.Description ?? string.Empty,
                CreateBy = OrderStatusDto.CreateBy ?? string.Empty,
                CreateDate = Timestamp.FromDateTime(DateTime.UtcNow)
            };
            
            byte[] fileData = null;
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileData = ms.ToArray();
                }
            }
            
            var response = await _orderGrpcClient.CreateOrderStatusAsync(orderStatusGrpc, fileData);
            
            // Map gRPC response to application DTO
            Response.IsSuccessed = response.IsSucceeded;
            Response.Message = response.Message;
            if (response.Object != null)
            {
                Response.Object = response.Object.Value;
            }

            return new JsonResult(Response);
        }

        #endregion

        #region Order Edit

        public async Task<IActionResult> OnPostOrderShippingChange()
        {
            // Use gRPC client
            var request = new OrderShippingChangeRequest
            {
                Type = "Change",
                UserShippingId = UserShippingId,
                OrderId = OrderId,
                OrderShippingId = 0
            };
            
            var response = await _orderGrpcClient.ChangeOrderShippingAsync(request);
            
            // Map gRPC response to application DTO
            Response.IsSuccessed = response.IsSucceeded;
            Response.Message = response.Message;
            if (response.Object != null)
            {
                Response.Object = response.Object.Value;
            }
            
            return new JsonResult(Response);
        }

        #endregion
        #region Word File
        public async Task<IActionResult> OnPostOrderWord()
        {
            // Use gRPC client
            var wordFile = await _orderGrpcClient.GenerateOrderWordFileAsync(OrderId);
            
            return Redirect("/" + wordFile.FilePath);
        }
        #endregion

        #region Get Statuses

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetStatuses(int OrderId)
        {
            // Use gRPC client
            var statusesResponse = await _orderGrpcClient.GetOrderStatusesAsync(OrderId);
            
            // Map gRPC response to application DTOs
            var statuses = statusesResponse.Statuses.Select(s => new OrderStatusDto
            {
                Id = s.Id,
                OrderId = s.OrderId,
                StatusId = s.StatusId,
                Description = s.Description,
                CreateBy = s.CreateBy,
                CreateDate = s.CreateDate?.ToDateTime()
            }).ToList();
            
            Response.Object = statuses;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region CompleteOrder
        public async Task<IActionResult> OnPostComplete(int orderId, string typeName, int? month)
        {
            // Use gRPC client
            var completeResponse = await _orderGrpcClient.CompleteOrderByAdminAsync(orderId, typeName, month);
            
            // Map gRPC response to application DTO
            var responseDto = new ResponseDto
            {
                IsSuccessed = completeResponse.IsSucceeded,
                Message = completeResponse.Message
            };
            
            if (completeResponse.Object != null)
            {
                responseDto.Object = completeResponse.Object.Value;
            }
            
            return new JsonResult(responseDto);
        }

        public async Task<IActionResult> OnPostOrderPays(int orderId)
        {
            // Use gRPC client
            var paysResponse = await _orderGrpcClient.GetOrderPaysAsync(orderId);
            
            // Map gRPC response to application DTOs
            var orderPays = paysResponse.Payments.Select(p => new OrderPayDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                PayTypeId = p.PayTypeId,
                Amount = p.Amount,
                RefId = p.RefId,
                Description = p.Description,
                CreateBy = p.CreateBy,
                CreateDate = p.CreateDate?.ToDateTime()
            }).ToList();
            
            return new JsonResult(orderPays);
        }
        
        public async Task<IActionResult> OnPostAddOrderPay()
        {
            // Use gRPC client
            var orderPayGrpc = new Parstech.Shop.Shared.Protos.Order.OrderPayDto
            {
                Id = orderPayDto.Id,
                OrderId = orderPayDto.OrderId,
                PayTypeId = orderPayDto.PayTypeId,
                Amount = orderPayDto.Amount,
                RefId = orderPayDto.RefId ?? string.Empty,
                Description = orderPayDto.Description ?? string.Empty,
                CreateBy = orderPayDto.CreateBy ?? string.Empty
            };
            
            if (orderPayDto.CreateDate.HasValue)
            {
                orderPayGrpc.CreateDate = Timestamp.FromDateTime(orderPayDto.CreateDate.Value.ToUniversalTime());
            }
            
            var response = await _orderGrpcClient.AddOrderPayAsync(orderPayGrpc);
            
            // Map gRPC response to application DTO
            var responseDto = new ResponseDto
            {
                IsSuccessed = response.IsSucceeded,
                Message = response.Message
            };
            
            if (response.Object != null)
            {
                responseDto.Object = response.Object.Value;
            }
            
            return new JsonResult(responseDto);
        }

        public async Task<IActionResult> OnPostDeleteOrderPay(int id)
        {
            // Use gRPC client
            var response = await _orderGrpcClient.DeleteOrderPayAsync(id);
            
            // Map gRPC response to application DTO
            var responseDto = new ResponseDto
            {
                IsSuccessed = response.IsSucceeded,
                Message = response.Message
            };
            
            if (response.Object != null)
            {
                responseDto.Object = response.Object.Value;
            }
            
            return new JsonResult(responseDto);
        }
        #endregion
        
        #region Mapping Methods
        
        private Parstech.Shop.Shared.Protos.Order.ParameterDto MapToParameterGrpc(Shop.Application.DTOs.Paging.ParameterDto parameter)
        {
            return new Parstech.Shop.Shared.Protos.Order.ParameterDto
            {
                CurrentPage = parameter.CurrentPage,
                TakePage = parameter.TakePage,
                SearchKey = parameter.SearchKey ?? string.Empty,
                StatusKey = parameter.StatusKey ?? string.Empty,
                PayTypeKey = parameter.PayTypeKey ?? string.Empty,
                StoreKey = parameter.StoreKey ?? string.Empty,
                CodeKey = parameter.CodeKey ?? string.Empty,
                CustomerKey = parameter.CustomerKey ?? string.Empty,
                FromDate = parameter.FromDate ?? string.Empty,
                ToDate = parameter.ToDate ?? string.Empty,
                Store = parameter.store ?? string.Empty
            };
        }
        
        private Shop.Application.DTOs.Paging.PagingDto MapFromPagingDtoGrpc(Parstech.Shop.Shared.Protos.Order.PagingDto pagingDto)
        {
            var result = new Shop.Application.DTOs.Paging.PagingDto
            {
                TotalCount = pagingDto.TotalCount,
                PageCount = pagingDto.PageCount,
                CurrentPage = pagingDto.CurrentPage,
                TakePage = pagingDto.TakePage,
                Items = new List<Shop.Application.DTOs.Order.OrderDto>()
            };
            
            foreach (var item in pagingDto.Items)
            {
                result.Items.Add(new Shop.Application.DTOs.Order.OrderDto
                {
                    OrderId = item.OrderId,
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Costumer = item.Costumer,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    CreateDate = item.CreateDate?.ToDateTime(),
                    CreateDateShamsi = item.CreateDateShamsi,
                    OrderCode = item.OrderCode,
                    OrderSum = item.OrderSum,
                    Shipping = item.Shipping,
                    Tax = item.Tax,
                    Discount = item.Discount,
                    Total = item.Total,
                    IsFinaly = item.IsFinaly,
                    IntroCode = item.IntroCode?.Value,
                    IntroCoin = item.IntroCoin,
                    ConfirmPayment = item.ConfirmPayment?.Value,
                    FactorFile = item.FactorFile?.Value,
                    IsDelete = item.IsDelete,
                    TaxId = item.TaxId,
                    Status = item.Status,
                    StatusIcon = item.StatusIcon,
                    PayType = item.PayType,
                    TypeName = item.TypeName,
                    StatusName = item.StatusName
                });
            }
            
            return result;
        }
        
        private Shop.Application.DTOs.OrderDetail.OrderDetailShowDto MapFromOrderDetailShowGrpc(Parstech.Shop.Shared.Protos.Order.OrderDetailShow orderDetailShow)
        {
            // Create a basic mapping - expand based on your needs
            var result = new Shop.Application.DTOs.OrderDetail.OrderDetailShowDto();
            
            if (orderDetailShow.Order != null)
            {
                result.Order = new Shop.Application.DTOs.Order.OrderDto
                {
                    OrderId = orderDetailShow.Order.OrderId,
                    UserId = orderDetailShow.Order.UserId,
                    UserName = orderDetailShow.Order.UserName,
                    // Map other properties as needed
                };
            }
            
            // Map other properties and collections as needed
            
            return result;
        }
        
        private Shop.Application.DTOs.Order.OrderFilterDto MapFromOrderFilterGrpc(Parstech.Shop.Shared.Protos.Order.OrderFilter filter)
        {
            var result = new Shop.Application.DTOs.Order.OrderFilterDto
            {
                Stores = new List<Shop.Application.DTOs.Order.StoreFilterDto>(),
                Statuses = new List<Shop.Application.DTOs.Order.StatusFilterDto>(),
                Pays = new List<Shop.Application.DTOs.Order.PayFilterDto>(),
                OrderCodes = new List<Shop.Application.DTOs.Order.OrdercodeFilterDto>(),
                Customers = new List<Shop.Application.DTOs.Order.CustomerFilterDto>()
            };
            
            // Map stores
            if (filter.Stores != null)
            {
                foreach (var store in filter.Stores)
                {
                    result.Stores.Add(new Shop.Application.DTOs.Order.StoreFilterDto
                    {
                        StoreName = store.StoreName,
                        UserStoreId = store.UserStoreId,
                        UserId = store.UserId
                    });
                }
            }
            
            // Map statuses
            if (filter.Statuses != null)
            {
                foreach (var status in filter.Statuses)
                {
                    result.Statuses.Add(new Shop.Application.DTOs.Order.StatusFilterDto
                    {
                        Id = status.Id,
                        StatusName = status.StatusName,
                        UserName = status.UserName
                    });
                }
            }
            
            // Map pays
            if (filter.Pays != null)
            {
                foreach (var pay in filter.Pays)
                {
                    result.Pays.Add(new Shop.Application.DTOs.Order.PayFilterDto
                    {
                        Id = pay.Id,
                        TypeName = pay.TypeName
                    });
                }
            }
            
            // Map order codes
            if (filter.Ordercodes != null)
            {
                foreach (var code in filter.Ordercodes)
                {
                    result.OrderCodes.Add(new Shop.Application.DTOs.Order.OrdercodeFilterDto
                    {
                        OrderCode = code.OrderCode
                    });
                }
            }
            
            // Map customers
            if (filter.Customers != null)
            {
                foreach (var customer in filter.Customers)
                {
                    result.Customers.Add(new Shop.Application.DTOs.Order.CustomerFilterDto
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName
                    });
                }
            }
            
            return result;
        }
        
        #endregion
    }
}
