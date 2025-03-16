using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.ProductInventoryAdmin;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.PriceConflictsLog.Requests.Queries;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Application.Features.Store.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parstech.Shop.ApiService.Services.GrpcServices
{
    public class ProductInventoryAdminGrpcService : ProductInventoryAdminService.ProductInventoryAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IProductStockPriceRepository _productStockPriceRepository;

        public ProductInventoryAdminGrpcService(
            IMediator mediator,
            IProductStockPriceRepository productStockPriceRepository)
        {
            _mediator = mediator;
            _productStockPriceRepository = productStockPriceRepository;
        }

        public override async Task<ProductStockPriceResponse> GetProductStockPrice(ProductStockPriceRequest request, ServerCallContext context)
        {
            try
            {
                var stockPrice = await _mediator.Send(new ProductStockPriceReadCommandReq(request.StockPrice.Id));
                
                return new ProductStockPriceResponse
                {
                    IsSuccess = true,
                    StockPrice = MapToDto(stockPrice)
                };
            }
            catch (Exception ex)
            {
                return new ProductStockPriceResponse
                {
                    IsSuccess = false,
                    Message = $"Failed to retrieve stock price: {ex.Message}"
                };
            }
        }

        public override async Task<ProductStockPriceListResponse> GetProductStockPrices(ProductIdRequest request, ServerCallContext context)
        {
            try
            {
                var stockPrices = await _mediator.Send(new ProductStockPricesReadByProductQueryReq(request.ProductId));
                
                var response = new ProductStockPriceListResponse
                {
                    IsSuccess = true
                };

                foreach (var stockPrice in stockPrices)
                {
                    response.StockPrices.Add(MapToDto(stockPrice));
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ProductStockPriceListResponse
                {
                    IsSuccess = false,
                    Message = $"Failed to retrieve stock prices: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseDto> CreateProductStockPrice(ProductStockPriceRequest request, ServerCallContext context)
        {
            try
            {
                var stockPriceDto = MapFromDto(request.StockPrice);
                var result = await _mediator.Send(new ProductStockPriceCreateCommandReq(stockPriceDto));
                
                // Refresh parent product quantity
                await _mediator.Send(new RefreshParentQuantityQueryReq(result.Id));

                return new ResponseDto
                {
                    IsSuccessed = true,
                    Message = "Stock price created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Failed to create stock price: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseDto> UpdateProductStockPrice(ProductStockPriceRequest request, ServerCallContext context)
        {
            try
            {
                var stockPriceDto = MapFromDto(request.StockPrice);
                var result = await _mediator.Send(new ProductStockPriceUpdateCommandReq(stockPriceDto));
                
                // Refresh parent product quantity
                await _mediator.Send(new RefreshParentQuantityQueryReq(result.Id));

                return new ResponseDto
                {
                    IsSuccessed = true,
                    Message = "Stock price updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Failed to update stock price: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseDto> DeleteProductStockPrice(ProductStockPriceIdRequest request, ServerCallContext context)
        {
            try
            {
                await _mediator.Send(new ProductStockPriceDeleteCommandReq(request.StockPriceId));
                
                return new ResponseDto
                {
                    IsSuccessed = true,
                    Message = "Stock price deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Failed to delete stock price: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseDto> QuickEditProductStockPrice(QuickEditRequest request, ServerCallContext context)
        {
            try
            {
                // Get current stock price
                var currentStockPrice = await _mediator.Send(new ProductStockPriceReadCommandReq(request.Id));
                var currentDto = new Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto
                {
                    Id = currentStockPrice.Id,
                    ProductId = currentStockPrice.ProductId,
                    StoreId = currentStockPrice.StoreId,
                    StoreName = currentStockPrice.StoreName,
                    RepId = currentStockPrice.RepId,
                    RepName = currentStockPrice.RepName,
                    Quantity = currentStockPrice.Quantity,
                    Price = currentStockPrice.Price,
                    SalePrice = currentStockPrice.SalePrice,
                    SpecialPrice = currentStockPrice.SpecialPrice,
                    ExpireDate = currentStockPrice.ExpireDate,
                    StockNumber = currentStockPrice.StockNumber,
                    SpecialDate = currentStockPrice.SpecialDate
                };

                // Update values
                currentStockPrice.Price = request.Price;
                currentStockPrice.SalePrice = request.SalePrice;
                currentStockPrice.Quantity = request.Quantity;

                // Save changes
                var edit = await _mediator.Send(new ProductStockPriceUpdateCommandReq(currentStockPrice));
                
                // Log price conflict if username provided
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    await _mediator.Send(new PriceConflictsCreateLogQueryReq(request.UserName, currentDto, edit));
                    
                    // Create representation if needed
                    var user = await _mediator.Send(new UserReadByUserNameQueryReq(request.UserName));
                    if (user != null)
                    {
                        var pr = new Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto
                        {
                            ProductStockPriceId = edit.Id,
                            UserId = user.Id
                        };
                        await _mediator.Send(new ProductRepresesntationQuickCreateCommandReq(pr));
                    }
                }

                return new ResponseDto
                {
                    IsSuccessed = true,
                    Message = "Stock price updated successfully via quick edit"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccessed = false,
                    Message = $"Failed to update stock price: {ex.Message}"
                };
            }
        }

        public override async Task<ProductStockPriceListResponse> GetProductInventoryByStore(StoreIdRequest request, ServerCallContext context)
        {
            try
            {
                var stockPrices = await _mediator.Send(new ProductStockPricesReadByStoreQueryReq(request.StoreId));
                
                var response = new ProductStockPriceListResponse
                {
                    IsSuccess = true
                };

                foreach (var stockPrice in stockPrices)
                {
                    response.StockPrices.Add(MapToDto(stockPrice));
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ProductStockPriceListResponse
                {
                    IsSuccess = false,
                    Message = $"Failed to retrieve store inventory: {ex.Message}"
                };
            }
        }

        public override async Task<StoreInventorySummaryResponse> GetStoreInventorySummary(StoreIdRequest request, ServerCallContext context)
        {
            try
            {
                var store = await _mediator.Send(new StoreReadCommandReq(request.StoreId));
                var stockPrices = await _mediator.Send(new ProductStockPricesReadByStoreQueryReq(request.StoreId));
                
                var response = new StoreInventorySummaryResponse
                {
                    IsSuccess = true,
                    StoreId = store.Id,
                    StoreName = store.Name,
                    TotalProducts = stockPrices.Count(),
                    TotalQuantity = stockPrices.Sum(s => s.Quantity),
                    TotalValue = stockPrices.Sum(s => s.Price * s.Quantity)
                };

                // Add top products (by value)
                var topProducts = stockPrices
                    .OrderByDescending(s => s.Price * s.Quantity)
                    .Take(10);

                foreach (var stockPrice in topProducts)
                {
                    response.TopProducts.Add(MapToDto(stockPrice));
                }

                return response;
            }
            catch (Exception ex)
            {
                return new StoreInventorySummaryResponse
                {
                    IsSuccess = false,
                    Message = $"Failed to retrieve store inventory summary: {ex.Message}"
                };
            }
        }

        // Helper methods to map between DTO types
        private ProductStockPriceDto MapToDto(Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto model)
        {
            if (model == null)
                return null;

            return new ProductStockPriceDto
            {
                Id = model.Id,
                ProductId = model.ProductId,
                StoreId = model.StoreId,
                StoreName = model.StoreName ?? string.Empty,
                RepId = model.RepId,
                RepName = model.RepName ?? string.Empty,
                Quantity = model.Quantity,
                Price = model.Price,
                SalePrice = model.SalePrice,
                SpecialPrice = model.SpecialPrice,
                ExpireDate = model.ExpireDate ?? string.Empty,
                StockNumber = model.StockNumber ?? string.Empty,
                SpecialDate = model.SpecialDate ?? string.Empty
            };
        }

        private Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto MapFromDto(ProductStockPriceDto model)
        {
            if (model == null)
                return null;

            return new Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto
            {
                Id = model.Id,
                ProductId = model.ProductId,
                StoreId = model.StoreId,
                StoreName = model.StoreName,
                RepId = model.RepId,
                RepName = model.RepName,
                Quantity = model.Quantity,
                Price = model.Price,
                SalePrice = model.SalePrice,
                SpecialPrice = model.SpecialPrice,
                ExpireDate = model.ExpireDate,
                StockNumber = model.StockNumber,
                SpecialDate = model.SpecialDate
            };
        }
    }
} 