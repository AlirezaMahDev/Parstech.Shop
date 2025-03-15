using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Parstech.Shop.Shared.Protos.FinancialAdmin;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class FinancialAdminGrpcClient : GrpcClientBase, IFinancialAdminGrpcClient
    {
        private readonly FinancialAdminService.FinancialAdminServiceClient _client;

        public FinancialAdminGrpcClient(IConfiguration configuration) : base(configuration)
        {
            _client = new FinancialAdminService.FinancialAdminServiceClient(Channel);
        }

        #region Wallet Operations

        public async Task<PagingDto> GetWalletsPagingAsync(ParameterDto parameter)
        {
            var request = new Parstech.Shop.Shared.Protos.FinancialAdmin.ParameterDto
            {
                PageId = parameter.PageId,
                Take = parameter.Take,
                SearchKey = parameter.SearchKey,
                Filter = parameter.Filter
            };

            var response = await _client.GetWalletsPagingAsync(request);

            // Deserialize the items from bytes
            var itemsString = Encoding.UTF8.GetString(response.Items.ToByteArray());
            var filterString = Encoding.UTF8.GetString(response.Filter.ToByteArray());

            var items = JsonConvert.DeserializeObject<List<object>>(itemsString);
            var filter = JsonConvert.DeserializeObject<object>(filterString);

            return new PagingDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code,
                Items = items,
                TotalRow = response.TotalRow,
                PageId = response.PageId,
                Take = response.Take,
                Filter = filter
            };
        }

        public async Task<List<UserFilterDto>> GetUserFiltersAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetUserFiltersAsync(request);

            var filters = new List<UserFilterDto>();

            foreach (var filter in response.Filters)
            {
                filters.Add(new UserFilterDto
                {
                    Id = filter.Id,
                    Name = filter.Name,
                    UserName = filter.UserName,
                    NameValue = filter.NameValue
                });
            }

            return filters;
        }

        public async Task<bool> BlockOrUnblockWalletAsync(bool isBlocked, int walletId)
        {
            var request = new BlockWalletRequest
            {
                IsBlocked = isBlocked,
                WalletId = walletId
            };

            var response = await _client.BlockOrUnblockWalletAsync(request);
            return response.Status;
        }

        #endregion

        #region Wallet Transactions

        public async Task<PagingDto> GetWalletTransactionsPagingAsync(WalletTransactionParameterDto parameter)
        {
            var request = new Parstech.Shop.Shared.Protos.FinancialAdmin.WalletTransactionParameterDto
            {
                PageId = parameter.PageId,
                Take = parameter.Take,
                SearchKey = parameter.SearchKey,
                Filter = parameter.Filter,
                WalletId = parameter.WalletId,
                DateFrom = parameter.DateFrom,
                DateTo = parameter.DateTo,
                TypeId = parameter.TypeId
            };

            var response = await _client.GetWalletTransactionsPagingAsync(request);

            // Deserialize the items from bytes
            var itemsString = Encoding.UTF8.GetString(response.Items.ToByteArray());
            var filterString = Encoding.UTF8.GetString(response.Filter.ToByteArray());

            var items = JsonConvert.DeserializeObject<List<object>>(itemsString);
            var filter = JsonConvert.DeserializeObject<object>(filterString);

            return new PagingDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code,
                Items = items,
                TotalRow = response.TotalRow,
                PageId = response.PageId,
                Take = response.Take,
                Filter = filter
            };
        }

        public async Task<ResponseDto> CreateWalletTransactionAsync(WalletTransactionDto transaction)
        {
            var request = new Parstech.Shop.Shared.Protos.FinancialAdmin.WalletTransactionDto
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

            var response = await _client.CreateWalletTransactionAsync(request);

            var result = new ResponseDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code
            };

            if (!string.IsNullOrEmpty(response.ObjectString))
            {
                result.Object = JsonConvert.DeserializeObject(response.ObjectString);
            }

            return result;
        }

        public async Task<WalletTransactionDto> GetWalletTransactionDetailAsync(int transactionId)
        {
            var request = new TransactionIdRequest { TransactionId = transactionId };
            var response = await _client.GetWalletTransactionDetailAsync(request);

            return new WalletTransactionDto
            {
                Id = response.Id,
                WalletId = response.WalletId,
                WalletOwner = response.WalletOwner,
                Amount = response.Amount,
                Description = response.Description,
                IsSuccess = response.IsSuccess,
                TypeId = response.TypeId,
                TypeName = response.TypeName,
                TransactionDate = response.TransactionDate,
                TrackingCode = response.TrackingCode,
                OrderId = response.OrderId,
                TransactionNumber = response.TransactionNumber,
                BankName = response.BankName,
                IsVerified = response.IsVerified
            };
        }

        #endregion

        #region Installment Operations

        public async Task<ResponseDto> PayInstallmentAsync(int transactionId)
        {
            var request = new TransactionIdRequest { TransactionId = transactionId };
            var response = await _client.PayInstallmentAsync(request);

            var result = new ResponseDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code
            };

            if (!string.IsNullOrEmpty(response.ObjectString))
            {
                result.Object = JsonConvert.DeserializeObject(response.ObjectString);
            }

            return result;
        }

        #endregion

        #region Facility Operations

        public async Task<ResponseDto> CreateFacilitiesAsync(FacilitiesDto facilities)
        {
            var request = new Parstech.Shop.Shared.Protos.FinancialAdmin.FacilitiesDto
            {
                Id = facilities.Id,
                WalletId = facilities.WalletId,
                WalletOwner = facilities.WalletOwner,
                Amount = facilities.Amount,
                Description = facilities.Description,
                NumberOfInstallments = facilities.NumberOfInstallments,
                FirstPaymentDate = facilities.FirstPaymentDate,
                MonthlyPayment = facilities.MonthlyPayment,
                WithInterest = facilities.WithInterest,
                InterestRate = facilities.InterestRate
            };

            var response = await _client.CreateFacilitiesAsync(request);

            var result = new ResponseDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code
            };

            if (!string.IsNullOrEmpty(response.ObjectString))
            {
                result.Object = JsonConvert.DeserializeObject(response.ObjectString);
            }

            return result;
        }

        public async Task<ResponseDto> RegisterFacilitiesByExcelAsync(string type, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var request = new ExcelUploadRequest
            {
                Type = type,
                FileData = ByteString.CopyFrom(memoryStream.ToArray()),
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            var response = await _client.RegisterFacilitiesByExcelAsync(request);

            var result = new ResponseDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code
            };

            if (!string.IsNullOrEmpty(response.ObjectString))
            {
                result.Object = JsonConvert.DeserializeObject(response.ObjectString);
            }

            return result;
        }

        public async Task<ResponseDto> ProcessFacilityPaymentsByExcelAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var request = new ExcelUploadRequest
            {
                FileData = ByteString.CopyFrom(memoryStream.ToArray()),
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            var response = await _client.ProcessFacilityPaymentsByExcelAsync(request);

            var result = new ResponseDto
            {
                Status = response.Status,
                Message = response.Message,
                Code = response.Code
            };

            if (!string.IsNullOrEmpty(response.ObjectString))
            {
                result.Object = JsonConvert.DeserializeObject(response.ObjectString);
            }

            return result;
        }

        #endregion
    }
} 