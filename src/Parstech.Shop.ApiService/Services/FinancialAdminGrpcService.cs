using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Parstech.Shop.Shared.Protos.FinancialAdmin;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class FinancialAdminGrpcService : FinancialAdminService.FinancialAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FinancialAdminGrpcService> _logger;

        public FinancialAdminGrpcService(
            IMediator mediator,
            ILogger<FinancialAdminGrpcService> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Wallet Operations

        public override async Task<PagingDto> GetWalletsPaging(ParameterDto request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.Paging.ParameterDto
                {
                    PageId = request.PageId,
                    Take = request.Take,
                    SearchKey = request.SearchKey,
                    Filter = request.Filter
                };

                var pagingResult = await _mediator.Send(new WalletPagingQueryReq(parameter));

                // Serialize the items to bytes
                var itemsBytes = ByteString.CopyFrom(JsonConvert.SerializeObject(pagingResult.Items), System.Text.Encoding.UTF8);
                var filterBytes = ByteString.CopyFrom(JsonConvert.SerializeObject(pagingResult.Filter), System.Text.Encoding.UTF8);

                return new PagingDto
                {
                    Status = pagingResult.Status,
                    Message = pagingResult.Message,
                    Code = pagingResult.Code,
                    Items = itemsBytes,
                    TotalRow = pagingResult.TotalRow,
                    PageId = pagingResult.PageId,
                    Take = pagingResult.Take,
                    Filter = filterBytes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wallets paging");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting wallets: {ex.Message}"));
            }
        }

        public override async Task<UserFiltersResponse> GetUserFilters(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var filters = await _mediator.Send(new UserFilterDataQueryReq());
                
                var response = new UserFiltersResponse();

                foreach (var filter in filters)
                {
                    response.Filters.Add(new UserFilterDto
                    {
                        Id = filter.Id,
                        Name = filter.Name,
                        UserName = filter.UserName,
                        NameValue = filter.NameValue
                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user filters");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting user filters: {ex.Message}"));
            }
        }

        public override async Task<ResponseDto> BlockOrUnblockWallet(BlockWalletRequest request, ServerCallContext context)
        {
            try
            {
                await _mediator.Send(new WalletBlockOrUnblockQueryReq(request.IsBlocked, request.WalletId));
                
                return new ResponseDto
                {
                    Status = true,
                    Message = request.IsBlocked ? "Wallet blocked successfully" : "Wallet unblocked successfully",
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error blocking/unblocking wallet");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error blocking/unblocking wallet: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion

        #region Wallet Transactions

        public override async Task<PagingDto> GetWalletTransactionsPaging(WalletTransactionParameterDto request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.WalletTransaction.WalletTransactionParameterDto
                {
                    PageId = request.PageId,
                    Take = request.Take,
                    SearchKey = request.SearchKey,
                    Filter = request.Filter,
                    WalletId = request.WalletId,
                    DateFrom = request.DateFrom,
                    DateTo = request.DateTo,
                    TypeId = request.TypeId
                };

                var pagingResult = await _mediator.Send(new WalletTransactionsPagingQueryReq(parameter));

                // Serialize the items to bytes
                var itemsBytes = ByteString.CopyFrom(JsonConvert.SerializeObject(pagingResult.Items), System.Text.Encoding.UTF8);
                var filterBytes = ByteString.CopyFrom(JsonConvert.SerializeObject(pagingResult.Filter), System.Text.Encoding.UTF8);

                return new PagingDto
                {
                    Status = pagingResult.Status,
                    Message = pagingResult.Message,
                    Code = pagingResult.Code,
                    Items = itemsBytes,
                    TotalRow = pagingResult.TotalRow,
                    PageId = pagingResult.PageId,
                    Take = pagingResult.Take,
                    Filter = filterBytes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wallet transactions paging");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting wallet transactions: {ex.Message}"));
            }
        }

        public override async Task<ResponseDto> CreateWalletTransaction(WalletTransactionDto request, ServerCallContext context)
        {
            try
            {
                var transaction = new Shop.Application.DTOs.WalletTransaction.WalletTransactionDto
                {
                    Id = request.Id,
                    WalletId = request.WalletId,
                    WalletOwner = request.WalletOwner,
                    Amount = request.Amount,
                    Description = request.Description,
                    IsSuccess = request.IsSuccess,
                    TypeId = request.TypeId,
                    TypeName = request.TypeName,
                    TransactionDate = request.TransactionDate,
                    TrackingCode = request.TrackingCode,
                    OrderId = request.OrderId,
                    TransactionNumber = request.TransactionNumber,
                    BankName = request.BankName,
                    IsVerified = request.IsVerified
                };

                var result = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction, true));

                return new ResponseDto
                {
                    Status = result.Status,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = JsonConvert.SerializeObject(result.Object)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating wallet transaction");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error creating wallet transaction: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<WalletTransactionDto> GetWalletTransactionDetail(TransactionIdRequest request, ServerCallContext context)
        {
            try
            {
                var transaction = await _mediator.Send(new WalletTransactionDetailShowQueryReq(request.TransactionId));

                return new WalletTransactionDto
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    WalletOwner = transaction.WalletOwner,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    IsSuccess = transaction.IsSuccess,
                    TypeId = transaction.TypeId,
                    TypeName = transaction.TypeName,
                    TransactionDate = transaction.TransactionDate,
                    TrackingCode = transaction.TrackingCode,
                    OrderId = transaction.OrderId,
                    TransactionNumber = transaction.TransactionNumber,
                    BankName = transaction.BankName,
                    IsVerified = transaction.IsVerified
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wallet transaction detail");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting wallet transaction detail: {ex.Message}"));
            }
        }

        #endregion

        #region Installment Operations

        public override async Task<ResponseDto> PayInstallment(TransactionIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new GhestPaymentQueryReq(request.TransactionId));

                return new ResponseDto
                {
                    Status = result.Status,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = JsonConvert.SerializeObject(result.Object)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error paying installment");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error paying installment: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion

        #region Facility Operations

        public override async Task<ResponseDto> CreateFacilities(FacilitiesDto request, ServerCallContext context)
        {
            try
            {
                // Facilities functionality is currently commented out in the original code
                // Uncomment and implement when needed
                return new ResponseDto
                {
                    Status = false,
                    Message = "Facilities creation is not currently implemented",
                    Code = 501
                };
                
                // When implementing, use the code below as a template:
                /*
                var facilities = new Shop.Application.DTOs.WalletTransaction.FacilitiesDto
                {
                    // Map properties from request
                };
                var result = await _mediator.Send(new CreateAghsatQueryReq(facilities));
                return new ResponseDto
                {
                    Status = result.Status,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = JsonConvert.SerializeObject(result.Object)
                };
                */
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating facilities");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error creating facilities: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> RegisterFacilitiesByExcel(ExcelUploadRequest request, ServerCallContext context)
        {
            try
            {
                // Create an IFormFile from the request data
                var fileData = new MemoryStream(request.FileData.ToByteArray());
                var formFile = new FormFile(
                    baseStream: fileData,
                    baseStreamOffset: 0,
                    length: fileData.Length,
                    name: "file",
                    fileName: request.FileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = request.ContentType
                };

                var result = await _mediator.Send(new FacilityRegistrationByExcelQueryReq(request.Type, formFile));

                return new ResponseDto
                {
                    Status = result.Status,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = JsonConvert.SerializeObject(result.Object)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering facilities by Excel");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error registering facilities by Excel: {ex.Message}",
                    Code = 500
                };
            }
        }

        public override async Task<ResponseDto> ProcessFacilityPaymentsByExcel(ExcelUploadRequest request, ServerCallContext context)
        {
            try
            {
                // Create an IFormFile from the request data
                var fileData = new MemoryStream(request.FileData.ToByteArray());
                var formFile = new FormFile(
                    baseStream: fileData,
                    baseStreamOffset: 0,
                    length: fileData.Length,
                    name: "file",
                    fileName: request.FileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = request.ContentType
                };

                var result = await _mediator.Send(new FacilityPaymentByExcelQueryReq(formFile));

                return new ResponseDto
                {
                    Status = result.Status,
                    Message = result.Message,
                    Code = result.Code,
                    ObjectString = JsonConvert.SerializeObject(result.Object)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing facility payments by Excel");
                return new ResponseDto
                {
                    Status = false,
                    Message = $"Error processing facility payments by Excel: {ex.Message}",
                    Code = 500
                };
            }
        }

        #endregion
    }
} 