using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Parstech.Shop.Shared.Protos.OrderAdmin;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.OrderPay.Request.Command;
using Shop.Application.Features.OrderPay.Request.Queries;
using Shop.Application.Features.OrderShipping.Request.Queries;
using Shop.Application.Features.OrderStatus.Requests.Commands;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.Rahkaran.Requests.Commands;
using Shop.Application.Features.Rahkaran.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Parstech.Shop.ApiService.Services
{
    public class OrderAdminGrpcService : OrderAdminService.OrderAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderAdminGrpcService> _logger;
        private readonly IWebHostEnvironment _environment;

        public OrderAdminGrpcService(
            IMediator mediator,
            ILogger<OrderAdminGrpcService> logger,
            IWebHostEnvironment environment)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        #region Order Listing and Filtering

        public override async Task<OrderFiltersResponse> GetOrderFilters(StringRequest request, ServerCallContext context)
        {
            try
            {
                var filters = await _mediator.Send(new GetOrderFilterDataQueryReq(request.Value));
                
                var response = new OrderFiltersResponse();
                foreach (var filter in filters)
                {
                    response.Filters.Add(new OrderFilterDto
                    {
                        Title = filter.Title,
                        Value = filter.Value
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order filters");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order filters: {ex.Message}"));
            }
        }

        public override async Task<OrderPagingDto> GetOrdersPaging(OrderParameterDto request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.Paging.OrderParameterDto
                {
                    PageId = request.PageId,
                    Take = request.Take,
                    SearchKey = request.SearchKey,
                    Filter = request.Filter,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    PaymentStatus = request.PaymentStatus,
                    StoreName = request.StoreName
                };
                
                var pagingResult = await _mediator.Send(new OrderPagingQueryReq(parameter));
                
                var response = new OrderPagingDto
                {
                    IsSuccessed = pagingResult.IsSuccessed,
                    Message = pagingResult.Message,
                    Code = pagingResult.Code,
                    TotalRow = pagingResult.TotalRow,
                    PageId = pagingResult.PageId,
                    Take = pagingResult.Take,
                    PendingCount = pagingResult.PendingCount,
                    AwaitingPaymentCount = pagingResult.AwaitingPaymentCount,
                    ProcessingCount = pagingResult.ProcessingCount,
                    CancelledCount = pagingResult.CancelledCount,
                    ShippedCount = pagingResult.ShippedCount,
                    DeliveredCount = pagingResult.DeliveredCount,
                    FailedCount = pagingResult.FailedCount,
                    RefundedCount = pagingResult.RefundedCount,
                    ReturnedCount = pagingResult.ReturnedCount,
                    AllCount = pagingResult.AllCount,
                    Parameter = new OrderParameterDto
                    {
                        PageId = pagingResult.Parameter.PageId,
                        Take = pagingResult.Parameter.Take,
                        SearchKey = pagingResult.Parameter.SearchKey,
                        Filter = pagingResult.Parameter.Filter,
                        FromDate = pagingResult.Parameter.FromDate,
                        ToDate = pagingResult.Parameter.ToDate,
                        PaymentStatus = pagingResult.Parameter.PaymentStatus,
                        StoreName = pagingResult.Parameter.StoreName
                    }
                };
                
                foreach (var order in pagingResult.Items)
                {
                    response.Items.Add(MapOrderToGrpc(order));
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders paging");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting orders: {ex.Message}"));
            }
        }

        #endregion

        #region Order Details

        public override async Task<OrderDetailDto> GetOrderDetails(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var orderDetail = await _mediator.Send(new OrderDetailQueryReq(request.OrderId));
                
                var response = new OrderDetailDto
                {
                    Order = MapOrderToGrpc(orderDetail.Order),
                    Address = orderDetail.Address,
                    Province = orderDetail.Province,
                    City = orderDetail.City,
                    PostalCode = orderDetail.PostalCode,
                    RecipientName = orderDetail.RecipientName,
                    RecipientMobile = orderDetail.RecipientMobile,
                    JalaliDeliveredDate = orderDetail.JalaliDeliveredDate,
                    JalaliOrderedDate = orderDetail.JalaliOrderedDate,
                    JalaliPaidDate = orderDetail.JalaliPaidDate,
                    JalaliShippedDate = orderDetail.JalaliShippedDate
                };
                
                foreach (var item in orderDetail.Items)
                {
                    response.Items.Add(new OrderItemDto
                    {
                        Id = item.Id,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ProductImage = item.ProductImage ?? "",
                        Quantity = item.Quantity,
                        Price = item.Price,
                        FinalPrice = item.FinalPrice,
                        DiscountPrice = item.DiscountPrice,
                        TaxPrice = item.TaxPrice,
                        IsShipping = item.IsShipping,
                        Description = item.Description ?? "",
                        Accepted = item.Accepted,
                        IsDownload = item.IsDownload,
                        Properties = item.Properties ?? "",
                        OriginalPrices = item.OriginalPrices ?? "",
                        Url = item.Url ?? ""
                    });
                }
                
                foreach (var status in orderDetail.Statuses)
                {
                    response.Statuses.Add(new OrderStatusDto
                    {
                        Id = status.Id,
                        OrderId = status.OrderId,
                        Status = status.Status,
                        StatusTitle = status.StatusTitle,
                        Description = status.Description ?? "",
                        CreatedDate = status.CreatedDate,
                        IsCurrent = status.IsCurrent,
                        FileName = status.FileName ?? ""
                    });
                }
                
                foreach (var shipping in orderDetail.Shippings)
                {
                    response.Shippings.Add(new OrderShippingDto
                    {
                        Id = shipping.Id,
                        OrderId = shipping.OrderId,
                        ShippingType = shipping.ShippingType,
                        ShippingTypeTitle = shipping.ShippingTypeTitle,
                        ShippingCost = shipping.ShippingCost,
                        Description = shipping.Description ?? "",
                        CreatedDate = shipping.CreatedDate,
                        IsCurrent = shipping.IsCurrent,
                        TrackingCode = shipping.TrackingCode ?? ""
                    });
                }
                
                foreach (var pay in orderDetail.Pays)
                {
                    response.Pays.Add(new OrderPayDto
                    {
                        Id = pay.Id,
                        OrderId = pay.OrderId,
                        Amount = pay.Amount,
                        Gateway = pay.Gateway ?? "",
                        PaymentDate = pay.PaymentDate,
                        TrackingCode = pay.TrackingCode ?? "",
                        IsSuccess = pay.IsSuccess,
                        Description = pay.Description ?? ""
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order details");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order details: {ex.Message}"));
            }
        }

        public override async Task<OrderStatusesResponse> GetOrderStatuses(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var statuses = await _mediator.Send(new GetOrderStatusesQueryReq(request.OrderId));
                
                var response = new OrderStatusesResponse();
                foreach (var status in statuses)
                {
                    response.Statuses.Add(new OrderStatusDto
                    {
                        Id = status.Id,
                        OrderId = status.OrderId,
                        Status = status.Status,
                        StatusTitle = status.StatusTitle,
                        Description = status.Description ?? "",
                        CreatedDate = status.CreatedDate,
                        IsCurrent = status.IsCurrent,
                        FileName = status.FileName ?? ""
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order statuses");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order statuses: {ex.Message}"));
            }
        }

        #endregion

        #region Order Modification

        public override async Task<ResponseDto> CreateOrderStatus(OrderStatusCreateRequest request, ServerCallContext context)
        {
            try
            {
                var orderStatus = new OrderStatusDto
                {
                    OrderId = request.OrderId,
                    Status = request.Status,
                    Description = request.Description
                };
                
                // Handle file upload if provided
                IFormFile file = null;
                if (request.FileData != null && request.FileData.Length > 0)
                {
                    var fileData = new MemoryStream(request.FileData.ToByteArray());
                    file = new FormFile(
                        baseStream: fileData,
                        baseStreamOffset: 0,
                        length: fileData.Length,
                        name: "file",
                        fileName: request.FileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = request.ContentType
                    };
                }
                
                var result = await _mediator.Send(new CreateOrderStatusCommandReq(orderStatus, file));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order status");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error creating order status: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> ChangeOrderShipping(ChangeOrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                var shipping = new OrderShippingDto
                {
                    OrderId = request.OrderId,
                    ShippingType = request.ShippingType,
                    ShippingCost = request.ShippingCost,
                    Description = request.Description,
                    TrackingCode = request.TrackingCode
                };
                
                var result = await _mediator.Send(new CreateOrderShippingCommandReq(shipping));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing order shipping");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error changing order shipping: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> CompleteOrderByAdmin(CompleteOrderRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new CompleteOrderCommandReq(request.OrderId, request.TypeName, request.Month));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing order by admin");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error completing order by admin: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion

        #region Order Document Generation

        public override async Task<FileResponse> GenerateOrderWordFile(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var orderDetail = await _mediator.Send(new OrderDetailQueryReq(request.OrderId));
                
                // Generate a Word document from order details
                var fileName = $"Order_{request.OrderId}.docx";
                var filePath = Path.Combine(_environment.WebRootPath, "temp", fileName);
                
                // Create the temp directory if it doesn't exist
                Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "temp"));
                
                // Create a new document
                using (var document = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    // Add a main document part
                    var mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    var body = new Body();
                    
                    // Add order header
                    var headerParagraph = new Paragraph(
                        new Run(
                            new Text($"Order Invoice #{orderDetail.Order.InvoiceNumber}")
                        )
                    );
                    body.AppendChild(headerParagraph);
                    
                    // Add customer info
                    body.AppendChild(new Paragraph(new Run(new Text($"Customer: {orderDetail.Order.UserFullName}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Mobile: {orderDetail.Order.UserMobile}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Order Date: {orderDetail.JalaliOrderedDate}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Status: {orderDetail.Order.StatusTitle}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Payment Status: {orderDetail.Order.PaymentStatusTitle}"))));
                    
                    // Address info
                    body.AppendChild(new Paragraph(new Run(new Text("Shipping Address:"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"{orderDetail.Address}, {orderDetail.City}, {orderDetail.Province}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Postal Code: {orderDetail.PostalCode}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Recipient: {orderDetail.RecipientName}, {orderDetail.RecipientMobile}"))));
                    
                    // Add order items
                    body.AppendChild(new Paragraph(new Run(new Text("Order Items:"))));
                    foreach (var item in orderDetail.Items)
                    {
                        body.AppendChild(new Paragraph(new Run(new Text(
                            $"{item.ProductName} x {item.Quantity} - Unit Price: {item.Price}, Total: {item.FinalPrice}"))));
                    }
                    
                    // Add order summary
                    body.AppendChild(new Paragraph(new Run(new Text("Order Summary:"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Subtotal: {orderDetail.Order.TotalPrice}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Discount: {orderDetail.Order.OrderDiscountPrice + orderDetail.Order.ItemsDiscountPrice}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Tax: {orderDetail.Order.TaxPrice}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Shipping: {orderDetail.Order.ShippingPrice}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Final Price: {orderDetail.Order.FinalPrice}"))));
                    
                    mainPart.Document.AppendChild(body);
                }
                
                // Read the generated file
                byte[] fileBytes = File.ReadAllBytes(filePath);
                
                // Clean up temporary file
                File.Delete(filePath);
                
                return new FileResponse
                {
                    FileName = fileName,
                    FileData = ByteString.CopyFrom(fileBytes),
                    ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating order word file");
                throw new RpcException(new Status(StatusCode.Internal, $"Error generating order word file: {ex.Message}"));
            }
        }

        #endregion

        #region Order Payment Management

        public override async Task<OrderPaysResponse> GetOrderPays(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var pays = await _mediator.Send(new GetOrderPaysQueryReq(request.OrderId));
                
                var response = new OrderPaysResponse();
                foreach (var pay in pays)
                {
                    response.Pays.Add(new OrderPayDto
                    {
                        Id = pay.Id,
                        OrderId = pay.OrderId,
                        Amount = pay.Amount,
                        Gateway = pay.Gateway ?? "",
                        PaymentDate = pay.PaymentDate,
                        TrackingCode = pay.TrackingCode ?? "",
                        IsSuccess = pay.IsSuccess,
                        Description = pay.Description ?? ""
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order pays");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order pays: {ex.Message}"));
            }
        }

        public override async Task<ResponseDto> AddOrderPay(OrderPayDto request, ServerCallContext context)
        {
            try
            {
                var orderPay = new OrderPayDto
                {
                    OrderId = request.OrderId,
                    Amount = request.Amount,
                    Gateway = request.Gateway,
                    PaymentDate = request.PaymentDate,
                    TrackingCode = request.TrackingCode,
                    IsSuccess = request.IsSuccess,
                    Description = request.Description
                };
                
                var result = await _mediator.Send(new CreateOrderPayCommandReq(orderPay));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order pay");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error adding order pay: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> DeleteOrderPay(OrderPayIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new DeleteOrderPayCommandReq(request.PayId));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order pay");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error deleting order pay: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion

        #region Rahkaran API Operations

        public override async Task<RahkaranOrderDto> GetRahkaranOrder(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var rahkaranOrder = await _mediator.Send(new GetRahkaranOrderQueryReq(request.OrderId));
                
                var response = new RahkaranOrderDto
                {
                    Id = rahkaranOrder.Id,
                    OrderId = rahkaranOrder.OrderId,
                    InvoiceNumber = rahkaranOrder.InvoiceNumber ?? "",
                    OrderDate = rahkaranOrder.OrderDate ?? "",
                    CustomerCode = rahkaranOrder.CustomerCode ?? "",
                    TotalPrice = rahkaranOrder.TotalPrice,
                    TaxPrice = rahkaranOrder.TaxPrice,
                    DiscountPrice = rahkaranOrder.DiscountPrice,
                    Description = rahkaranOrder.Description ?? "",
                    IsVerified = rahkaranOrder.IsVerified,
                    FollowingNumber = rahkaranOrder.FollowingNumber,
                    CustomerName = rahkaranOrder.CustomerName ?? ""
                };
                
                foreach (var item in rahkaranOrder.Items)
                {
                    response.Items.Add(new RahkaranOrderItemDto
                    {
                        Id = item.Id,
                        RahkaranOrderId = item.RahkaranOrderId,
                        OrderItemId = item.OrderItemId,
                        ProductCode = item.ProductCode ?? "",
                        ProductName = item.ProductName ?? "",
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        DiscountPrice = item.DiscountPrice,
                        TaxPrice = item.TaxPrice,
                        FinalPrice = item.FinalPrice,
                        Description = item.Description ?? ""
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Rahkaran order");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting Rahkaran order: {ex.Message}"));
            }
        }

        public override async Task<RahkaranUserDto> GetRahkaranUser(UserIdRequest request, ServerCallContext context)
        {
            try
            {
                var rahkaranUser = await _mediator.Send(new GetRahkaranUserQueryReq(request.UserId));
                
                return new RahkaranUserDto
                {
                    Id = rahkaranUser.Id,
                    UserId = rahkaranUser.UserId,
                    Code = rahkaranUser.Code ?? "",
                    Name = rahkaranUser.Name ?? "",
                    Family = rahkaranUser.Family ?? "",
                    Mobile = rahkaranUser.Mobile ?? "",
                    EconomicCode = rahkaranUser.EconomicCode ?? "",
                    NationalCode = rahkaranUser.NationalCode ?? "",
                    Address = rahkaranUser.Address ?? "",
                    Phone = rahkaranUser.Phone ?? "",
                    PostalCode = rahkaranUser.PostalCode ?? "",
                    IsVerified = rahkaranUser.IsVerified
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Rahkaran user");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting Rahkaran user: {ex.Message}"));
            }
        }

        public override async Task<RahkaranProductDto> GetRahkaranProduct(ProductIdRequest request, ServerCallContext context)
        {
            try
            {
                var rahkaranProduct = await _mediator.Send(new GetRahkaranProductQueryReq(request.ProductId));
                
                return new RahkaranProductDto
                {
                    Id = rahkaranProduct.Id,
                    ProductId = rahkaranProduct.ProductId,
                    Code = rahkaranProduct.Code ?? "",
                    Name = rahkaranProduct.Name ?? "",
                    Description = rahkaranProduct.Description ?? "",
                    Type = rahkaranProduct.Type ?? "",
                    Group = rahkaranProduct.Group ?? "",
                    ParentCode = rahkaranProduct.ParentCode ?? "",
                    ParentName = rahkaranProduct.ParentName ?? "",
                    IsVerified = rahkaranProduct.IsVerified
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Rahkaran product");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting Rahkaran product: {ex.Message}"));
            }
        }

        public override async Task<ResponseDto> SendOrderToRahkaran(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new SendOrderToApiCommandReq(request.OrderId));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending order to Rahkaran");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error sending order to Rahkaran: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> FollowOrderFromRahkaran(OrderIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new FollowOrderFromApiCommandReq(request.OrderId));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error following order from Rahkaran");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error following order from Rahkaran: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> CreateRahkaranOrder(RahkaranOrderDto request, ServerCallContext context)
        {
            try
            {
                var rahkaranOrder = new RahkaranOrderDto
                {
                    OrderId = request.OrderId,
                    InvoiceNumber = request.InvoiceNumber,
                    OrderDate = request.OrderDate,
                    CustomerCode = request.CustomerCode,
                    TotalPrice = request.TotalPrice,
                    TaxPrice = request.TaxPrice,
                    DiscountPrice = request.DiscountPrice,
                    Description = request.Description,
                    IsVerified = request.IsVerified,
                    FollowingNumber = request.FollowingNumber,
                    CustomerName = request.CustomerName,
                    Items = new List<RahkaranOrderItemDto>()
                };
                
                foreach (var item in request.Items)
                {
                    rahkaranOrder.Items.Add(new RahkaranOrderItemDto
                    {
                        OrderItemId = item.OrderItemId,
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        DiscountPrice = item.DiscountPrice,
                        TaxPrice = item.TaxPrice,
                        FinalPrice = item.FinalPrice,
                        Description = item.Description
                    });
                }
                
                var result = await _mediator.Send(new CreateRahkaranOrderCommandReq(rahkaranOrder));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Rahkaran order");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error creating Rahkaran order: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> CreateRahkaranUser(RahkaranUserDto request, ServerCallContext context)
        {
            try
            {
                var rahkaranUser = new RahkaranUserDto
                {
                    UserId = request.UserId,
                    Code = request.Code,
                    Name = request.Name,
                    Family = request.Family,
                    Mobile = request.Mobile,
                    EconomicCode = request.EconomicCode,
                    NationalCode = request.NationalCode,
                    Address = request.Address,
                    Phone = request.Phone,
                    PostalCode = request.PostalCode,
                    IsVerified = request.IsVerified
                };
                
                var result = await _mediator.Send(new CreateRahkaranUserCommandReq(rahkaranUser));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Rahkaran user");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error creating Rahkaran user: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> CreateRahkaranProduct(RahkaranProductDto request, ServerCallContext context)
        {
            try
            {
                var rahkaranProduct = new RahkaranProductDto
                {
                    ProductId = request.ProductId,
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    Group = request.Group,
                    ParentCode = request.ParentCode,
                    ParentName = request.ParentName,
                    IsVerified = request.IsVerified
                };
                
                var result = await _mediator.Send(new CreateRahkaranProductCommandReq(rahkaranProduct));
                
                return new ResponseDto
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = result.Object != null ? result.Object.ToString() : string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Rahkaran product");
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Error creating Rahkaran product: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion

        #region Helper Methods

        private OrderDto MapOrderToGrpc(Shop.Application.DTOs.Order.OrderDto order)
        {
            return new OrderDto
            {
                Id = order.Id,
                InvoiceNumber = order.InvoiceNumber ?? "",
                UserId = order.UserId,
                UserFullName = order.UserFullName ?? "",
                UserMobile = order.UserMobile ?? "",
                Status = order.Status ?? "",
                StatusTitle = order.StatusTitle ?? "",
                PaymentStatus = order.PaymentStatus ?? "",
                PaymentStatusTitle = order.PaymentStatusTitle ?? "",
                ShippingStatus = order.ShippingStatus ?? "",
                ShippingStatusTitle = order.ShippingStatusTitle ?? "",
                ShippingType = order.ShippingType ?? "",
                ShippingTypeTitle = order.ShippingTypeTitle ?? "",
                OrderedDate = order.OrderedDate ?? "",
                ShippedDate = order.ShippedDate ?? "",
                DeliveredDate = order.DeliveredDate ?? "",
                PaymentDate = order.PaymentDate ?? "",
                TotalPrice = order.TotalPrice,
                OrderDiscountPrice = order.OrderDiscountPrice,
                ItemsDiscountPrice = order.ItemsDiscountPrice,
                TaxPrice = order.TaxPrice,
                ShippingPrice = order.ShippingPrice,
                FinalPrice = order.FinalPrice,
                Description = order.Description ?? "",
                ItemCount = order.ItemCount,
                HasPhysicalProduct = order.HasPhysicalProduct,
                StoreName = order.StoreName ?? "",
                IsOrderVerified = order.IsOrderVerified,
                IsPayVerified = order.IsPayVerified,
                Fullname = order.Fullname ?? "",
                TrackingCode = order.TrackingCode ?? ""
            };
        }

        #endregion
    }
} 