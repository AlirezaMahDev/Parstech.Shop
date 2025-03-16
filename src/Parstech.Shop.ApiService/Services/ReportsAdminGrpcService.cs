using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Google.Protobuf;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Parstech.Shop.Shared.Protos.ReportsAdmin;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class ReportsAdminGrpcService : ReportsAdminService.ReportsAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ReportsAdminGrpcService> _logger;

        public ReportsAdminGrpcService(
            IMediator mediator,
            ILogger<ReportsAdminGrpcService> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<UsersForSelectListResponse> GetUsersForSelectList(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var users = await _mediator.Send(new GetUsersForSelectListQueryReq());
                
                var response = new UsersForSelectListResponse();
                
                foreach (var user in users)
                {
                    response.Users.Add(new UserForSelectListDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Mobile = user.Mobile,
                        Fullname = user.Fullname
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users for select list");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting users: {ex.Message}"));
            }
        }

        public override async Task<WalletTransactionPagingDto> GetTransactionsReport(TransactionParameterDto request, ServerCallContext context)
        {
            try
            {
                var parameter = MapToApplicationTransactionParameter(request);
                
                var result = await _mediator.Send(new ReportOfWalletTransactionsQueryReq(parameter));
                
                return MapToGrpcWalletTransactionPaging(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting transactions report");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting transactions report: {ex.Message}"));
            }
        }

        public override async Task<WalletTransactionPagingDto> GetActiveCreditReport(TransactionParameterDto request, ServerCallContext context)
        {
            try
            {
                var parameter = MapToApplicationTransactionParameter(request);
                
                var result = await _mediator.Send(new ReportOfActiveCreditQueryReq(parameter));
                
                return MapToGrpcWalletTransactionPaging(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active credit report");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting active credit report: {ex.Message}"));
            }
        }

        public override async Task<WalletTransactionPagingDto> GetActiveInstallments(UserIdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new GetAghsatQueryReq(request.UserId));
                
                return MapToGrpcWalletTransactionPaging(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active installments");
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting active installments: {ex.Message}"));
            }
        }

        public override async Task<ExcelResponse> GenerateTransactionsExcel(TransactionReportExcelRequest request, ServerCallContext context)
        {
            try
            {
                var parameters = new TransactionParameterDto
                {
                    UserFilter = request.UserFilter,
                    WalletType = request.WalletType,
                    TransactionType = request.TransactionType,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate
                };
                
                var parameter = MapToApplicationTransactionParameter(parameters);
                
                // Get report data
                var result = await _mediator.Send(new ReportOfWalletTransactionsQueryReq(parameter));
                
                // Generate Excel file
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Transactions");
                
                // Add headers
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Wallet Owner";
                worksheet.Cell(1, 3).Value = "Amount";
                worksheet.Cell(1, 4).Value = "Description";
                worksheet.Cell(1, 5).Value = "Type";
                worksheet.Cell(1, 6).Value = "Date";
                worksheet.Cell(1, 7).Value = "Price";
                worksheet.Cell(1, 8).Value = "Wallet Credit";
                
                // Add data
                int row = 2;
                foreach (var item in result.Items)
                {
                    worksheet.Cell(row, 1).Value = item.Id;
                    worksheet.Cell(row, 2).Value = item.WalletOwner;
                    worksheet.Cell(row, 3).Value = item.Amount;
                    worksheet.Cell(row, 4).Value = item.Description;
                    worksheet.Cell(row, 5).Value = item.TypeName;
                    worksheet.Cell(row, 6).Value = item.JalaliDate;
                    worksheet.Cell(row, 7).Value = item.Price;
                    worksheet.Cell(row, 8).Value = item.WalletCredit;
                    row++;
                }
                
                // Save to memory stream
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;
                
                return new ExcelResponse
                {
                    ExcelData = ByteString.FromStream(stream),
                    FileName = "Transactions_Report.xlsx",
                    IsSuccess = true,
                    Message = "Excel file generated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating transactions excel");
                return new ExcelResponse
                {
                    IsSuccess = false,
                    Message = $"Error generating transactions excel: {ex.Message}"
                };
            }
        }

        public override async Task<ExcelResponse> GenerateActiveCreditExcel(TransactionReportExcelRequest request, ServerCallContext context)
        {
            try
            {
                var parameters = new TransactionParameterDto
                {
                    UserFilter = request.UserFilter,
                    WalletType = request.WalletType,
                    TransactionType = request.TransactionType,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate
                };
                
                var parameter = MapToApplicationTransactionParameter(parameters);
                
                // Get report data
                var result = await _mediator.Send(new ReportOfActiveCreditQueryReq(parameter));
                
                // Generate Excel file
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ActiveCredit");
                
                // Add headers
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Wallet Owner";
                worksheet.Cell(1, 3).Value = "Amount";
                worksheet.Cell(1, 4).Value = "Description";
                worksheet.Cell(1, 5).Value = "Type";
                worksheet.Cell(1, 6).Value = "Date";
                worksheet.Cell(1, 7).Value = "Paid";
                worksheet.Cell(1, 8).Value = "Remaining";
                
                // Add data
                int row = 2;
                foreach (var item in result.Items)
                {
                    worksheet.Cell(row, 1).Value = item.Id;
                    worksheet.Cell(row, 2).Value = item.WalletOwner;
                    worksheet.Cell(row, 3).Value = item.Amount;
                    worksheet.Cell(row, 4).Value = item.Description;
                    worksheet.Cell(row, 5).Value = item.TypeName;
                    worksheet.Cell(row, 6).Value = item.JalaliDate;
                    worksheet.Cell(row, 7).Value = item.Pay;
                    worksheet.Cell(row, 8).Value = item.Price;
                    row++;
                }
                
                // Save to memory stream
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;
                
                return new ExcelResponse
                {
                    ExcelData = ByteString.FromStream(stream),
                    FileName = "ActiveCredit_Report.xlsx",
                    IsSuccess = true,
                    Message = "Excel file generated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating active credit excel");
                return new ExcelResponse
                {
                    IsSuccess = false,
                    Message = $"Error generating active credit excel: {ex.Message}"
                };
            }
        }

        #region Mapping Helpers

        private Shop.Application.DTOs.WalletTransaction.TransactionParameterDto MapToApplicationTransactionParameter(TransactionParameterDto request)
        {
            return new Shop.Application.DTOs.WalletTransaction.TransactionParameterDto
            {
                CurrentPage = request.CurrentPage,
                TakePage = request.TakePage,
                UserFilter = request.UserFilter,
                WalletType = request.WalletType,
                TransactionType = request.TransactionType,
                FromDate = request.FromDate,
                ToDate = request.ToDate
            };
        }

        private WalletTransactionPagingDto MapToGrpcWalletTransactionPaging(Shop.Application.DTOs.WalletTransaction.WalletTransactionPagingDto result)
        {
            var response = new WalletTransactionPagingDto
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
                TotalRow = result.TotalRow,
                PageId = result.PageId,
                Take = result.Take,
                TotalPrice = result.TotalPrice,
                Walletbalance = result.Walletbalance,
                Parameter = result.Parameter != null ? new TransactionParameterDto
                {
                    CurrentPage = result.Parameter.CurrentPage,
                    TakePage = result.Parameter.TakePage,
                    UserFilter = result.Parameter.UserFilter,
                    WalletType = result.Parameter.WalletType,
                    TransactionType = result.Parameter.TransactionType,
                    FromDate = result.Parameter.FromDate,
                    ToDate = result.Parameter.ToDate
                } : null
            };

            if (result.Items != null)
            {
                foreach (var item in result.Items)
                {
                    response.Items.Add(new WalletTransactionReportDto
                    {
                        Id = item.Id,
                        WalletId = item.WalletId,
                        WalletOwner = item.WalletOwner,
                        Amount = item.Amount,
                        Description = item.Description,
                        IsSuccess = item.IsSuccess,
                        TypeId = item.TypeId,
                        TypeName = item.TypeName,
                        TransactionDate = item.TransactionDate,
                        TrackingCode = item.TrackingCode,
                        OrderId = item.OrderId,
                        TransactionNumber = item.TransactionNumber,
                        BankName = item.BankName,
                        JalaliDate = item.JalaliDate,
                        Price = item.Price,
                        BankPrice = item.BankPrice,
                        WalletCredit = item.WalletCredit,
                        Pay = item.Pay,
                        IsVerified = item.IsVerified,
                        WalletName = item.WalletName,
                        IsPayed = item.IsPayed,
                        UserId = item.UserId,
                        CreatedDate = item.CreatedDate
                    });
                }
            }

            return response;
        }

        #endregion
    }
} 