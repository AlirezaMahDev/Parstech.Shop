using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.StoreAdmin;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.ApiService.Services
{
    public class StoreAdminGrpcService : StoreAdminService.StoreAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StoreAdminGrpcService> _logger;

        public StoreAdminGrpcService(IMediator mediator, ILogger<StoreAdminGrpcService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get sales data for store
        /// </summary>
        public override async Task<SalesPagingDto> GetSalesForStore(SalesParameterRequest request, ServerCallContext context)
        {
            try
            {
                var parameters = new SalesParameterDto
                {
                    CurrentPage = request.CurrentPage,
                    TakePage = request.TakePage,
                    Filter = request.Filter,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    StoreId = request.StoreId
                };

                var result = await _mediator.Send(new OrderDetailsForStoreReportQueryReq(parameters, request.IsAdmin));

                var response = new SalesPagingDto
                {
                    CurrentPage = result.CurrentPage,
                    PageCount = result.PageCount,
                    RowCount = result.RowCount
                };

                foreach (var item in result.List)
                {
                    response.List.Add(MapToSalesDto(item));
                }

                foreach (var store in result.StoresSelect)
                {
                    response.StoresSelect.Add(MapToUserStoreDto(store));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSalesForStore");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving sales data"));
            }
        }

        /// <summary>
        /// Get all user stores
        /// </summary>
        public override async Task<UserStoresResponse> GetUserStores(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var userStores = await _mediator.Send(new UserStoreReadsCommandReq());
                var response = new UserStoresResponse();

                foreach (var store in userStores)
                {
                    response.Stores.Add(MapToUserStoreDto(store));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserStores");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving user stores"));
            }
        }

        /// <summary>
        /// Get order statuses by order ID
        /// </summary>
        public override async Task<OrderStatusesResponse> GetOrderStatuses(OrderStatusRequest request, ServerCallContext context)
        {
            try
            {
                var statuses = await _mediator.Send(new GetOrderStatusByOrderIdQueryReq(request.OrderId));
                var response = new OrderStatusesResponse();

                foreach (var status in statuses)
                {
                    response.Statuses.Add(MapToOrderStatusDto(status));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOrderStatuses");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving order statuses"));
            }
        }

        /// <summary>
        /// Get contract for order
        /// </summary>
        public override async Task<ContractResponse> GetContractForOrder(ContractOrderRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new ContractOrderQueryReq(request.OrderId, request.StoreName));
                
                return new ContractResponse
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message ?? string.Empty,
                    ContractHtml = result.Object?.ToString() ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetContractForOrder");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while retrieving the contract"));
            }
        }

        #region Mapping Methods

        private SalesDto MapToSalesDto(Shop.Application.DTOs.OrderDetail.SalesDto source)
        {
            var result = new SalesDto
            {
                Id = source.Id,
                OrderId = source.OrderId,
                OrderNumber = source.OrderNumber ?? string.Empty,
                ProductName = source.ProductName ?? string.Empty,
                ProductId = source.ProductId,
                Count = source.Count,
                Price = source.Price,
                SumPrice = source.SumPrice,
                StoreId = source.StoreId,
                StoreName = source.StoreName ?? string.Empty,
                LatinStoreName = source.LatinStoreName ?? string.Empty,
                UserName = source.UserName ?? string.Empty,
                FullName = source.FullName ?? string.Empty,
                CreateDateShamsi = source.CreateDateShamsi ?? string.Empty,
                OrderStatusId = source.OrderStatusId,
                OrderStatusTitle = source.OrderStatusTitle ?? string.Empty
            };

            if (source.CreateDate.HasValue)
            {
                result.CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(source.CreateDate.Value.ToUniversalTime());
            }

            return result;
        }

        private UserStoreDto MapToUserStoreDto(Shop.Application.DTOs.UserStore.UserStoreDto source)
        {
            return new UserStoreDto
            {
                Id = source.Id,
                UserId = source.UserId ?? string.Empty,
                Name = source.Name ?? string.Empty,
                LatinName = source.LatinName ?? string.Empty,
                Mobile = source.Mobile ?? string.Empty,
                Logo = source.Logo ?? string.Empty,
                Address = source.Address ?? string.Empty,
                IsActive = source.IsActive
            };
        }

        private OrderStatusDto MapToOrderStatusDto(Shop.Application.DTOs.OrderStatus.OrderStatusDto source)
        {
            var result = new OrderStatusDto
            {
                Id = source.Id,
                OrderId = source.OrderId,
                StatusId = source.StatusId,
                StatusTitle = source.StatusTitle ?? string.Empty,
                Description = source.Description ?? string.Empty,
                File = source.File ?? string.Empty,
                CreateDateShamsi = source.CreateDateShamsi ?? string.Empty
            };

            if (source.CreateDate.HasValue)
            {
                result.CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(source.CreateDate.Value.ToUniversalTime());
            }

            return result;
        }

        #endregion
    }
} 