using Google.Protobuf;

using Grpc.Core;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class OrderAdminGrpcClient : IOrderAdminGrpcClient
{
    private readonly OrderAdminService.OrderAdminServiceClient _client;
    private readonly ILogger<OrderAdminGrpcClient> _logger;

    public OrderAdminGrpcClient(
        OrderAdminService.OrderAdminServiceClient client,
        ILogger<OrderAdminGrpcClient> logger)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Order Listing and Filtering

    public async Task<List<OrderFilterDataDto>> GetOrderFiltersAsync(string filter)
    {
        try
        {
            var request = new StringRequest { Value = filter };

            var response = await _client.GetOrderFiltersAsync(request);
            var result = new List<OrderFilterDataDto>();

            foreach (var filter in response.Filters)
            {
                result.Add(new OrderFilterDataDto { Title = filter.Title, Value = filter.Value });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting order filters");
            throw;
        }
    }

    public async Task<ResponsePagingDto<OrderDto, OrderParameterDto>> GetOrdersPagingAsync(OrderParameterDto parameter)
    {
        try
        {
            var request = new OrderParameterDto
            {
                PageId = parameter.PageId,
                Take = parameter.Take,
                SearchKey = parameter.SearchKey ?? "",
                Filter = parameter.Filter ?? "",
                FromDate = parameter.FromDate ?? "",
                ToDate = parameter.ToDate ?? "",
                PaymentStatus = parameter.PaymentStatus ?? "",
                StoreName = parameter.StoreName ?? ""
            };

            var response = await _client.GetOrdersPagingAsync(request);

            var result = new ResponsePagingDto<OrderDto, OrderParameterDto>
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                TotalRow = response.TotalRow,
                PageId = response.PageId,
                Take = response.Take,
                PendingCount = response.PendingCount,
                AwaitingPaymentCount = response.AwaitingPaymentCount,
                ProcessingCount = response.ProcessingCount,
                CancelledCount = response.CancelledCount,
                ShippedCount = response.ShippedCount,
                DeliveredCount = response.DeliveredCount,
                FailedCount = response.FailedCount,
                RefundedCount = response.RefundedCount,
                ReturnedCount = response.ReturnedCount,
                AllCount = response.AllCount,
                Items = new List<OrderDto>(),
                Parameter = new OrderParameterDto
                {
                    PageId = response.Parameter.PageId,
                    Take = response.Parameter.Take,
                    SearchKey = response.Parameter.SearchKey,
                    Filter = response.Parameter.Filter,
                    FromDate = response.Parameter.FromDate,
                    ToDate = response.Parameter.ToDate,
                    PaymentStatus = response.Parameter.PaymentStatus,
                    StoreName = response.Parameter.StoreName
                }
            };

            foreach (var order in response.Items)
            {
                result.Items.Add(MapOrderFromGrpc(order));
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting orders paging");
            throw;
        }
    }

    #endregion

    #region Order Details

    public async Task<OrderDetailDto> GetOrderDetailsAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.GetOrderDetailsAsync(request);

            var result = new OrderDetailDto
            {
                Order = MapOrderFromGrpc(response.Order),
                Address = response.Address,
                Province = response.Province,
                City = response.City,
                PostalCode = response.PostalCode,
                RecipientName = response.RecipientName,
                RecipientMobile = response.RecipientMobile,
                JalaliDeliveredDate = response.JalaliDeliveredDate,
                JalaliOrderedDate = response.JalaliOrderedDate,
                JalaliPaidDate = response.JalaliPaidDate,
                JalaliShippedDate = response.JalaliShippedDate,
                Items = new List<OrderItemDto>(),
                Statuses = new List<OrderStatusDto>(),
                Shippings = new List<OrderShippingDto>(),
                Pays = new List<OrderPayDto>()
            };

            foreach (var item in response.Items)
            {
                result.Items.Add(new OrderItemDto
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductImage = item.ProductImage,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    FinalPrice = item.FinalPrice,
                    DiscountPrice = item.DiscountPrice,
                    TaxPrice = item.TaxPrice,
                    IsShipping = item.IsShipping,
                    Description = item.Description,
                    Accepted = item.Accepted,
                    IsDownload = item.IsDownload,
                    Properties = item.Properties,
                    OriginalPrices = item.OriginalPrices,
                    Url = item.Url
                });
            }

            foreach (var status in response.Statuses)
            {
                result.Statuses.Add(new OrderStatusDto
                {
                    Id = status.Id,
                    OrderId = status.OrderId,
                    Status = status.Status,
                    StatusTitle = status.StatusTitle,
                    Description = status.Description,
                    CreatedDate = status.CreatedDate,
                    IsCurrent = status.IsCurrent,
                    FileName = status.FileName
                });
            }

            foreach (var shipping in response.Shippings)
            {
                result.Shippings.Add(new OrderShippingDto
                {
                    Id = shipping.Id,
                    OrderId = shipping.OrderId,
                    ShippingType = shipping.ShippingType,
                    ShippingTypeTitle = shipping.ShippingTypeTitle,
                    ShippingCost = shipping.ShippingCost,
                    Description = shipping.Description,
                    CreatedDate = shipping.CreatedDate,
                    IsCurrent = shipping.IsCurrent,
                    TrackingCode = shipping.TrackingCode
                });
            }

            foreach (var pay in response.Pays)
            {
                result.Pays.Add(new OrderPayDto
                {
                    Id = pay.Id,
                    OrderId = pay.OrderId,
                    Amount = pay.Amount,
                    Gateway = pay.Gateway,
                    PaymentDate = pay.PaymentDate,
                    TrackingCode = pay.TrackingCode,
                    IsSuccess = pay.IsSuccess,
                    Description = pay.Description
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting order details");
            throw;
        }
    }

    public async Task<List<OrderStatusDto>> GetOrderStatusesAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.GetOrderStatusesAsync(request);

            var result = new List<OrderStatusDto>();
            foreach (var status in response.Statuses)
            {
                result.Add(new()
                {
                    Id = status.Id,
                    OrderId = status.OrderId,
                    Status = status.Status,
                    StatusTitle = status.StatusTitle,
                    Description = status.Description,
                    CreatedDate = status.CreatedDate,
                    IsCurrent = status.IsCurrent,
                    FileName = status.FileName
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting order statuses");
            throw;
        }
    }

    #endregion

    #region Order Modification

    public async Task<ResponseDto> CreateOrderStatusAsync(OrderStatusDto orderStatus, IFormFile file)
    {
        try
        {
            var request = new OrderStatusCreateRequest
            {
                OrderId = orderStatus.OrderId,
                Status = orderStatus.Status,
                Description = orderStatus.Description ?? ""
            };

            // Handle file if provided
            if (file != null)
            {
                using (MemoryStream ms = new())
                {
                    await file.CopyToAsync(ms);
                    request.FileData = ByteString.CopyFrom(ms.ToArray());
                    request.FileName = file.FileName;
                    request.ContentType = file.ContentType;
                }
            }

            var response = await _client.CreateOrderStatusAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error creating order status");
            return new()
            {
                IsSuccessed = false, Message = $"Error creating order status: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> ChangeOrderShippingAsync(OrderShippingDto shipping)
    {
        try
        {
            var request = new ChangeOrderShippingRequest
            {
                OrderId = shipping.OrderId,
                ShippingType = shipping.ShippingType,
                ShippingCost = shipping.ShippingCost,
                Description = shipping.Description ?? "",
                TrackingCode = shipping.TrackingCode ?? ""
            };

            var response = await _client.ChangeOrderShippingAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error changing order shipping");
            return new()
            {
                IsSuccessed = false, Message = $"Error changing order shipping: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> CompleteOrderByAdminAsync(int orderId, string typeName, int month)
    {
        try
        {
            var request = new CompleteOrderRequest { OrderId = orderId, TypeName = typeName ?? "", Month = month };

            var response = await _client.CompleteOrderByAdminAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error completing order by admin");
            return new()
            {
                IsSuccessed = false, Message = $"Error completing order by admin: {ex.Message}", Code = 500
            };
        }
    }

    #endregion

    #region Order Document Generation

    public async Task<byte[]> GenerateOrderWordFileAsync(int orderId, out string fileName)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.GenerateOrderWordFileAsync(request);

            fileName = response.FileName;
            return response.FileData.ToByteArray();
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error generating order word file");
            fileName = $"Order_{orderId}_error.docx";
            throw;
        }
    }

    #endregion

    #region Order Payment Management

    public async Task<List<OrderPayDto>> GetOrderPaysAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.GetOrderPaysAsync(request);

            var result = new List<OrderPayDto>();
            foreach (var pay in response.Pays)
            {
                result.Add(new()
                {
                    Id = pay.Id,
                    OrderId = pay.OrderId,
                    Amount = pay.Amount,
                    Gateway = pay.Gateway,
                    PaymentDate = pay.PaymentDate,
                    TrackingCode = pay.TrackingCode,
                    IsSuccess = pay.IsSuccess,
                    Description = pay.Description
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting order pays");
            throw;
        }
    }

    public async Task<ResponseDto> AddOrderPayAsync(OrderPayDto orderPay)
    {
        try
        {
            var request = new OrderPayDto
            {
                OrderId = orderPay.OrderId,
                Amount = orderPay.Amount,
                Gateway = orderPay.Gateway ?? "",
                PaymentDate = orderPay.PaymentDate,
                TrackingCode = orderPay.TrackingCode ?? "",
                IsSuccess = orderPay.IsSuccess,
                Description = orderPay.Description ?? ""
            };

            var response = await _client.AddOrderPayAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error adding order pay");
            return new()
            {
                IsSuccessed = false, Message = $"Error adding order pay: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> DeleteOrderPayAsync(int payId)
    {
        try
        {
            var request = new OrderPayIdRequest { PayId = payId };
            var response = await _client.DeleteOrderPayAsync(request);

            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error deleting order pay");
            return new()
            {
                IsSuccessed = false, Message = $"Error deleting order pay: {ex.Message}", Code = 500
            };
        }
    }

    #endregion

    #region Rahkaran API Operations

    public async Task<RahkaranOrderDto> GetRahkaranOrderAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.GetRahkaranOrderAsync(request);

            var result = new RahkaranOrderDto
            {
                Id = response.Id,
                OrderId = response.OrderId,
                InvoiceNumber = response.InvoiceNumber,
                OrderDate = response.OrderDate,
                CustomerCode = response.CustomerCode,
                TotalPrice = response.TotalPrice,
                TaxPrice = response.TaxPrice,
                DiscountPrice = response.DiscountPrice,
                Description = response.Description,
                IsVerified = response.IsVerified,
                FollowingNumber = response.FollowingNumber,
                CustomerName = response.CustomerName,
                Items = new List<RahkaranOrderItemDto>()
            };

            foreach (var item in response.Items)
            {
                result.Items.Add(new RahkaranOrderItemDto
                {
                    Id = item.Id,
                    RahkaranOrderId = item.RahkaranOrderId,
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

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting Rahkaran order");
            throw;
        }
    }

    public async Task<RahkaranUserDto> GetRahkaranUserAsync(int userId)
    {
        try
        {
            var request = new UserIdRequest { UserId = userId };
            var response = await _client.GetRahkaranUserAsync(request);

            return new()
            {
                Id = response.Id,
                UserId = response.UserId,
                Code = response.Code,
                Name = response.Name,
                Family = response.Family,
                Mobile = response.Mobile,
                EconomicCode = response.EconomicCode,
                NationalCode = response.NationalCode,
                Address = response.Address,
                Phone = response.Phone,
                PostalCode = response.PostalCode,
                IsVerified = response.IsVerified
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting Rahkaran user");
            throw;
        }
    }

    public async Task<RahkaranProductDto> GetRahkaranProductAsync(int productId)
    {
        try
        {
            var request = new ProductIdRequest { ProductId = productId };
            var response = await _client.GetRahkaranProductAsync(request);

            return new()
            {
                Id = response.Id,
                ProductId = response.ProductId,
                Code = response.Code,
                Name = response.Name,
                Description = response.Description,
                Type = response.Type,
                Group = response.Group,
                ParentCode = response.ParentCode,
                ParentName = response.ParentName,
                IsVerified = response.IsVerified
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error getting Rahkaran product");
            throw;
        }
    }

    public async Task<ResponseDto> SendOrderToRahkaranAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.SendOrderToRahkaranAsync(request);

            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error sending order to Rahkaran");
            return new()
            {
                IsSuccessed = false, Message = $"Error sending order to Rahkaran: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> FollowOrderFromRahkaranAsync(int orderId)
    {
        try
        {
            var request = new OrderIdRequest { OrderId = orderId };
            var response = await _client.FollowOrderFromRahkaranAsync(request);

            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error following order from Rahkaran");
            return new()
            {
                IsSuccessed = false, Message = $"Error following order from Rahkaran: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> CreateRahkaranOrderAsync(RahkaranOrderDto order)
    {
        try
        {
            var request = new RahkaranOrderDto
            {
                OrderId = order.OrderId,
                InvoiceNumber = order.InvoiceNumber ?? "",
                OrderDate = order.OrderDate ?? "",
                CustomerCode = order.CustomerCode ?? "",
                TotalPrice = order.TotalPrice,
                TaxPrice = order.TaxPrice,
                DiscountPrice = order.DiscountPrice,
                Description = order.Description ?? "",
                IsVerified = order.IsVerified,
                FollowingNumber = order.FollowingNumber,
                CustomerName = order.CustomerName ?? ""
            };

            if (order.Items != null)
            {
                foreach (var item in order.Items)
                {
                    request.Items.Add(new RahkaranOrderItemDto
                    {
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
            }

            var response = await _client.CreateRahkaranOrderAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error creating Rahkaran order");
            return new()
            {
                IsSuccessed = false, Message = $"Error creating Rahkaran order: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> CreateRahkaranUserAsync(RahkaranUserDto user)
    {
        try
        {
            var request = new RahkaranUserDto
            {
                UserId = user.UserId,
                Code = user.Code ?? "",
                Name = user.Name ?? "",
                Family = user.Family ?? "",
                Mobile = user.Mobile ?? "",
                EconomicCode = user.EconomicCode ?? "",
                NationalCode = user.NationalCode ?? "",
                Address = user.Address ?? "",
                Phone = user.Phone ?? "",
                PostalCode = user.PostalCode ?? "",
                IsVerified = user.IsVerified
            };

            var response = await _client.CreateRahkaranUserAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error creating Rahkaran user");
            return new()
            {
                IsSuccessed = false, Message = $"Error creating Rahkaran user: {ex.Message}", Code = 500
            };
        }
    }

    public async Task<ResponseDto> CreateRahkaranProductAsync(RahkaranProductDto product)
    {
        try
        {
            var request = new RahkaranProductDto
            {
                ProductId = product.ProductId,
                Code = product.Code ?? "",
                Name = product.Name ?? "",
                Description = product.Description ?? "",
                Type = product.Type ?? "",
                Group = product.Group ?? "",
                ParentCode = product.ParentCode ?? "",
                ParentName = product.ParentName ?? "",
                IsVerified = product.IsVerified
            };

            var response = await _client.CreateRahkaranProductAsync(request);
            return new()
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Code = response.Code,
                Object = response.ObjectString
            };
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error creating Rahkaran product");
            return new()
            {
                IsSuccessed = false, Message = $"Error creating Rahkaran product: {ex.Message}", Code = 500
            };
        }
    }

    #endregion

    #region Helper Methods

    private OrderDto MapOrderFromGrpc(OrderDto orderGrpc)
    {
        return new()
        {
            Id = orderGrpc.Id,
            InvoiceNumber = orderGrpc.InvoiceNumber,
            UserId = orderGrpc.UserId,
            UserFullName = orderGrpc.UserFullName,
            UserMobile = orderGrpc.UserMobile,
            Status = orderGrpc.Status,
            StatusTitle = orderGrpc.StatusTitle,
            PaymentStatus = orderGrpc.PaymentStatus,
            PaymentStatusTitle = orderGrpc.PaymentStatusTitle,
            ShippingStatus = orderGrpc.ShippingStatus,
            ShippingStatusTitle = orderGrpc.ShippingStatusTitle,
            ShippingType = orderGrpc.ShippingType,
            ShippingTypeTitle = orderGrpc.ShippingTypeTitle,
            OrderedDate = orderGrpc.OrderedDate,
            ShippedDate = orderGrpc.ShippedDate,
            DeliveredDate = orderGrpc.DeliveredDate,
            PaymentDate = orderGrpc.PaymentDate,
            TotalPrice = orderGrpc.TotalPrice,
            OrderDiscountPrice = orderGrpc.OrderDiscountPrice,
            ItemsDiscountPrice = orderGrpc.ItemsDiscountPrice,
            TaxPrice = orderGrpc.TaxPrice,
            ShippingPrice = orderGrpc.ShippingPrice,
            FinalPrice = orderGrpc.FinalPrice,
            Description = orderGrpc.Description,
            ItemCount = orderGrpc.ItemCount,
            HasPhysicalProduct = orderGrpc.HasPhysicalProduct,
            StoreName = orderGrpc.StoreName,
            IsOrderVerified = orderGrpc.IsOrderVerified,
            IsPayVerified = orderGrpc.IsPayVerified,
            Fullname = orderGrpc.Fullname,
            TrackingCode = orderGrpc.TrackingCode
        };
    }

    #endregion
}