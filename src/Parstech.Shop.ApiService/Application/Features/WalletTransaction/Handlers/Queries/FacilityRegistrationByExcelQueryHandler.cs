using MediatR;

using OfficeOpenXml;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Queries;

public class FacilityRegistrationByExcelQueryHandler : IRequestHandler<FacilityRegistrationByExcelQueryReq, ResponseDto>
{
    private readonly IWalletRepository _walletRep;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IUserRepository _userRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMediator _mediator;

    public FacilityRegistrationByExcelQueryHandler(IWalletRepository walletRep,
        IWalletTransactionRepository walletTransactionRep,
        IUserRepository userRep,
        IUserBillingRepository userBillingRep,
        IMediator mediator)
    {
        _walletRep = walletRep;
        _walletTransactionRep = walletTransactionRep;
        _userRep = userRep;
        _userBillingRep = userBillingRep;
        _mediator = mediator;
    }

    public async Task<ResponseDto> Handle(FacilityRegistrationByExcelQueryReq request,
        CancellationToken cancellationToken)
    {
        string FilePath = "";
        List<ErrorList> errors = new();
        List<WalletTransactionDto> successList = new();
        ResponseDto Response = new();
        if (request.file != null)
        {
            try
            {
                string name = "excelFacilityRegistration" + Path.GetExtension(request.file.FileName);
                FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Files", name);
                using (FileStream stream = new(FilePath, FileMode.Create))
                {
                    request.file.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
            }
        }

        //var FileName = "sample.xlsx";
        //string webRootPath = _hostingEnvironment.WebRootPath;
        //string FilePath = Path.Combine(webRootPath, FileName);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (ExcelPackage package = new(new FileInfo(FilePath)))
        {
            ExcelWorksheet? worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;
            List<List<string>> DATA = new();
            for (int row = 1; row <= rowCount; row++)
            {
                List<string> rowData = new();
                for (int col = 1; col <= colCount; col++)
                {
                    rowData.Add(worksheet.Cells[row, col].Text);
                }

                DATA.Add(rowData);
            }

            foreach (List<string> rowData in DATA)
            {
                string personal = rowData[0];
                string price = rowData[1];
                string persent = rowData[2];
                int Finalpersent = 0;
                string month = rowData[3];
                int Finalmonth = 0;

                int userId = await _userBillingRep.ExistBillingForPersonalId(personal);
                if (userId == 0)
                {
                    ErrorList error = new() { Caption = personal, ErrorMessage = "کاربر در سامانه یافت نشد" };
                    errors.Add(error);
                }

                long longPrice = long.Parse(price);
                Type priceType = longPrice.GetType();
                if (longPrice < 0 && (priceType.Name != "int64" || priceType.Name != "int32"))
                {
                    ErrorList error = new() { Caption = personal, ErrorMessage = "مبلغ تسهیلات نادرست" };
                    errors.Add(error);
                }
            }

            if (errors.Count > 0)
            {
                Response.IsSuccessed = false;
                Response.Object = errors;
                return Response;
            }


            foreach (List<string> rowData in DATA)
            {
                string personal = rowData[0];
                string price = rowData[1];
                string persent = rowData[2];
                int Finalpersent = 0;
                string month = rowData[3];
                int Finalmonth = 0;

                int userId = await _userBillingRep.ExistBillingForPersonalId(personal);

                long longPrice = long.Parse(price);
                if (persent == "" || int.Parse(persent) < 0)
                {
                    Finalpersent = int.Parse(persent);
                }
                else
                {
                    Finalpersent = 0;
                }

                if (month == "" || int.Parse(month) < 0)
                {
                    Finalmonth = 0;
                }
                else
                {
                    Finalmonth = int.Parse(month);
                }

                if (errors.Count > 0)
                {
                    Response.IsSuccessed = false;
                    Response.Object = errors;
                    return Response;
                }

                WalletTransactionDto Transaction = new();
                Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(userId);
                Transaction.Price = int.Parse(price);
                Transaction.CreateDate = DateTime.Now;
                Transaction.WalletId = wallet.WalletId;
                Transaction.Persent = Finalpersent;
                Transaction.Month = Finalmonth;
                Transaction.Start = false;
                Transaction.Active = true;
                Transaction.Description = "ثبت تسهیلات جدید";
                Transaction.Type = request.type;
                Transaction.TypeId = 1;
                var result = await _mediator.Send(new CreateWalletTransactionCommandReq(Transaction, true));
                if (!result.isSuccessed)
                {
                    ErrorList error = new()
                    {
                        Caption = personal,
                        ErrorMessage = "تا زمانی که تسهیلات کاربر تسویه نگردد امکان ثبت تسهیلات جدید وجود ندارد"
                    };
                    errors.Add(error);
                }
            }

            Response.IsSuccessed = true;
            Response.Object = errors;
            return Response;
        }
    }
}