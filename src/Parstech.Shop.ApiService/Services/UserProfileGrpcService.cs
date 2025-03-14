using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.UserProfileService;
using Shop.Application.DTOs.Paging;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserShipping.Requests.Commands;
using Shop.Application.Features.UserShipping.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class UserProfileGrpcService : UserProfileService.UserProfileServiceBase
    {
        private readonly IMediator _mediator;
        
        public UserProfileGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<UserShippingListResponse> GetUserShippingAddresses(UserShippingRequest request, ServerCallContext context)
        {
            try
            {
                var shippings = await _mediator.Send(new UserShippingOfUserQueryReq(request.UserId));
                
                var response = new UserShippingListResponse();
                foreach (var shipping in shippings)
                {
                    response.ShippingAddresses.Add(new UserShippingResponse
                    {
                        Id = shipping.Id,
                        UserId = shipping.UserId,
                        Address = shipping.Address,
                        PostalCode = shipping.PostalCode,
                        Mobile = shipping.Mobile,
                        City = shipping.City,
                        Province = shipping.Province,
                        IsDefault = shipping.IsDefault
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<UserShippingResponse> GetUserShippingById(ShippingIdRequest request, ServerCallContext context)
        {
            try
            {
                var shipping = await _mediator.Send(new UserShippingReadCommandReq(request.ShippingId));
                
                return new UserShippingResponse
                {
                    Id = shipping.Id,
                    UserId = shipping.UserId,
                    Address = shipping.Address,
                    PostalCode = shipping.PostalCode,
                    Mobile = shipping.Mobile,
                    City = shipping.City,
                    Province = shipping.Province,
                    IsDefault = shipping.IsDefault
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<UserShippingResponse> CreateUserShipping(CreateShippingRequest request, ServerCallContext context)
        {
            try
            {
                var shippingDto = new Shop.Application.DTOs.UserShipping.UserShippingDto
                {
                    UserId = request.UserId,
                    Address = request.Address,
                    PostalCode = request.PostalCode,
                    Mobile = request.Mobile,
                    City = request.City,
                    Province = request.Province,
                    IsDefault = request.IsDefault
                };
                
                var result = await _mediator.Send(new UserShippingCreateCommandReq(shippingDto));
                
                return new UserShippingResponse
                {
                    Id = result.Id,
                    UserId = result.UserId,
                    Address = result.Address,
                    PostalCode = result.PostalCode,
                    Mobile = result.Mobile,
                    City = result.City,
                    Province = result.Province,
                    IsDefault = result.IsDefault
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<UserShippingResponse> UpdateUserShipping(UpdateShippingRequest request, ServerCallContext context)
        {
            try
            {
                var shippingDto = new Shop.Application.DTOs.UserShipping.UserShippingDto
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    Address = request.Address,
                    PostalCode = request.PostalCode,
                    Mobile = request.Mobile,
                    City = request.City,
                    Province = request.Province,
                    IsDefault = request.IsDefault
                };
                
                var result = await _mediator.Send(new UserShippingUpdateCommandReq(shippingDto));
                
                return new UserShippingResponse
                {
                    Id = result.Id,
                    UserId = result.UserId,
                    Address = result.Address,
                    PostalCode = result.PostalCode,
                    Mobile = result.Mobile,
                    City = result.City,
                    Province = result.Province,
                    IsDefault = result.IsDefault
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<DeleteShippingResponse> DeleteUserShipping(ShippingIdRequest request, ServerCallContext context)
        {
            try
            {
                await _mediator.Send(new UserShippingDeleteCommandReq(request.ShippingId));
                
                return new DeleteShippingResponse
                {
                    Success = true,
                    Message = "آدرس شما با موفقیت حذف گردید"
                };
            }
            catch (Exception ex)
            {
                return new DeleteShippingResponse
                {
                    Success = false,
                    Message = $"خطا در حذف آدرس: {ex.Message}"
                };
            }
        }
        
        public override async Task<UserOrdersResponse> GetUserOrdersHistory(UserOrdersRequest request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.Paging.ParameterDto
                {
                    CurrentPage = request.Page,
                    TakePage = request.PageSize,
                    SearchKey = request.SearchTerm
                };
                
                var result = await _mediator.Send(new FinallyOrdersOfUserByPagingQueryReq(request.UserId, parameter));
                
                var response = new UserOrdersResponse
                {
                    TotalCount = result.RowCount,
                    PageCount = result.PageCount,
                    CurrentPage = result.CurrentPage
                };
                
                foreach (var order in result.List)
                {
                    response.Orders.Add(new OrderSummary
                    {
                        OrderId = order.OrderId,
                        TrackingCode = order.TrackingCode,
                        CreateDate = order.CreateDate?.ToString() ?? string.Empty,
                        IsPaid = order.IsPaid,
                        Total = order.Total,
                        Discount = order.Discount,
                        FinalPrice = order.FinalPrice,
                        Status = order.Status
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<OrderDetailsResponse> GetOrderDetails(OrderDetailsRequest request, ServerCallContext context)
        {
            try
            {
                var orderDetails = await _mediator.Send(new OrderDetailShowQueryReq(request.OrderId));
                
                var response = new OrderDetailsResponse
                {
                    OrderId = orderDetails.OrderId,
                    UserName = orderDetails.UserName,
                    Total = orderDetails.Total,
                    Discount = orderDetails.Discount,
                    FinalPrice = orderDetails.FinalPrice,
                    ShippingId = orderDetails.ShippingId,
                    ShippingAddress = orderDetails.ShippingAddress,
                    ShippingPostalCode = orderDetails.ShippingPostalCode,
                    ShippingMobile = orderDetails.ShippingMobile
                };
                
                foreach (var detail in orderDetails.OrderDetails)
                {
                    response.Details.Add(new OrderDetailItem
                    {
                        Id = detail.Id,
                        OrderId = detail.OrderId,
                        ProductId = detail.ProductId,
                        ProductName = detail.ProductName,
                        ProductImage = detail.ProductImage,
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
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<UserTransactionsResponse> GetUserTransactions(UserTransactionsRequest request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.WalletTransaction.WalletTransactionParameterDto
                {
                    WalletId = request.WalletId,
                    CurrentPage = request.Page,
                    TakePage = request.PageSize,
                    Type = request.TransactionType
                };
                
                var result = await _mediator.Send(new WalletTransactionsPagingQueryReq(parameter));
                
                var response = new UserTransactionsResponse
                {
                    TotalCount = result.RowCount,
                    PageCount = result.PageCount,
                    CurrentPage = result.CurrentPage
                };
                
                foreach (var transaction in result.List)
                {
                    response.Transactions.Add(new TransactionSummary
                    {
                        TransactionId = transaction.TransactionId,
                        TransactionDate = transaction.TransactionDate?.ToString() ?? string.Empty,
                        Amount = transaction.Amount,
                        TypeName = transaction.TypeName,
                        Description = transaction.Description,
                        IsCredit = transaction.IsCredit
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<TransactionDetailsResponse> GetTransactionDetails(TransactionDetailsRequest request, ServerCallContext context)
        {
            try
            {
                var transaction = await _mediator.Send(new WalletTransactionDetailShowQueryReq(request.TransactionId));
                
                return new TransactionDetailsResponse
                {
                    TransactionId = transaction.TransactionId,
                    WalletId = transaction.WalletId,
                    TypeName = transaction.TypeName,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    TrackingCode = transaction.TrackingCode,
                    TransactionDate = transaction.TransactionDate?.ToString() ?? string.Empty,
                    Months = transaction.Months,
                    MonthlyPayment = transaction.MonthlyPayment,
                    IsActive = transaction.IsActive,
                    IsCredit = transaction.IsCredit
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
} 