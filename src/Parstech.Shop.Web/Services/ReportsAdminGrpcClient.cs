using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class ReportsAdminGrpcClient : GrpcClientBase, IReportsAdminGrpcClient
{
    private readonly ReportsAdminService.ReportsAdminServiceClient _client;

    public ReportsAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new ReportsAdminService.ReportsAdminServiceClient(Channel);
    }

    public async Task<List<UserForSelectListDto>> GetUsersForSelectListAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.GetUsersForSelectListAsync(request);

        var users = new List<UserForSelectListDto>();

        foreach (var user in response.Users)
        {
            users.Add(new()
            {
                Id = user.Id, Name = user.Name, Mobile = user.Mobile, Fullname = user.Fullname
            });
        }

        return users;
    }

    public async Task<WalletTransactionPagingDto> GetTransactionsReportAsync(TransactionParameterDto parameter)
    {
        var request = new Parstech.Shop.Shared.Protos.ReportsAdmin.TransactionParameterDto
        {
            CurrentPage = parameter.CurrentPage,
            TakePage = parameter.TakePage,
            UserFilter = parameter.UserFilter,
            WalletType = parameter.WalletType,
            TransactionType = parameter.TransactionType,
            FromDate = parameter.FromDate,
            ToDate = parameter.ToDate
        };

        var response = await _client.GetTransactionsReportAsync(request);

        return MapFromGrpcWalletTransactionPaging(response);
    }

    public async Task<WalletTransactionPagingDto> GetActiveCreditReportAsync(TransactionParameterDto parameter)
    {
        var request = new Parstech.Shop.Shared.Protos.ReportsAdmin.TransactionParameterDto
        {
            CurrentPage = parameter.CurrentPage,
            TakePage = parameter.TakePage,
            UserFilter = parameter.UserFilter,
            WalletType = parameter.WalletType,
            TransactionType = parameter.TransactionType,
            FromDate = parameter.FromDate,
            ToDate = parameter.ToDate
        };

        var response = await _client.GetActiveCreditReportAsync(request);

        return MapFromGrpcWalletTransactionPaging(response);
    }

    public async Task<WalletTransactionPagingDto> GetActiveInstallmentsAsync(int userId)
    {
        var request = new UserIdRequest { UserId = userId };
        var response = await _client.GetActiveInstallmentsAsync(request);

        return MapFromGrpcWalletTransactionPaging(response);
    }

    public async Task<(byte[] FileData, string FileName)> GenerateTransactionsExcelAsync(string userFilter,
        string walletType,
        int transactionType,
        string fromDate,
        string toDate)
    {
        var request = new TransactionReportExcelRequest
        {
            UserFilter = userFilter,
            WalletType = walletType,
            TransactionType = transactionType,
            FromDate = fromDate,
            ToDate = toDate
        };

        var response = await _client.GenerateTransactionsExcelAsync(request);

        if (!response.IsSuccess)
        {
            throw new(response.Message);
        }

        return (response.ExcelData.ToByteArray(), response.FileName);
    }

    public async Task<(byte[] FileData, string FileName)> GenerateActiveCreditExcelAsync(string userFilter,
        string walletType,
        int transactionType,
        string fromDate,
        string toDate)
    {
        var request = new TransactionReportExcelRequest
        {
            UserFilter = userFilter,
            WalletType = walletType,
            TransactionType = transactionType,
            FromDate = fromDate,
            ToDate = toDate
        };

        var response = await _client.GenerateActiveCreditExcelAsync(request);

        if (!response.IsSuccess)
        {
            throw new(response.Message);
        }

        return (response.ExcelData.ToByteArray(), response.FileName);
    }

    #region Mapping Helpers

    private WalletTransactionPagingDto MapFromGrpcWalletTransactionPaging(
        Shop.Shared.Protos.ReportsAdmin.WalletTransactionPagingDto response)
    {
        var result = new WalletTransactionPagingDto
        {
            IsSuccessed = response.IsSuccessed,
            Message = response.Message,
            TotalRow = response.TotalRow,
            PageId = response.PageId,
            Take = response.Take,
            TotalPrice = response.TotalPrice,
            Walletbalance = response.Walletbalance,
            Parameter = response.Parameter != null
                ? new TransactionParameterDto
                {
                    CurrentPage = response.Parameter.CurrentPage,
                    TakePage = response.Parameter.TakePage,
                    UserFilter = response.Parameter.UserFilter,
                    WalletType = response.Parameter.WalletType,
                    TransactionType = response.Parameter.TransactionType,
                    FromDate = response.Parameter.FromDate,
                    ToDate = response.Parameter.ToDate
                }
                : null,
            Items = new List<WalletTransactionReportDto>()
        };

        foreach (var item in response.Items)
        {
            result.Items.Add(new WalletTransactionReportDto
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

        return result;
    }

    #endregion
}