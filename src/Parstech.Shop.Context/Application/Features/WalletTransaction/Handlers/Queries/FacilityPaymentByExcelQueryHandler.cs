using MediatR;
using OfficeOpenXml;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class FacilityPaymentByExcelQueryHandler : IRequestHandler<FacilityPaymentByExcelQueryReq, ResponseDto>
{
    private readonly IWalletRepository _walletRep;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IUserRepository _userRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMediator _mediator;
    public FacilityPaymentByExcelQueryHandler(IWalletRepository walletRep,
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
    public async Task<ResponseDto> Handle(FacilityPaymentByExcelQueryReq request, CancellationToken cancellationToken)
    {
        string FilePath = "";
        List<ErrorList> errors = new();
        List<WalletTransactionDto> successList = new();
        ResponseDto Response = new();
        if (request.file != null)
        {

            try
            {

                var name = "excelFacilityRegistration" + Path.GetExtension(request.file.FileName);
                FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Files", name);
                using (var stream = new FileStream(FilePath, FileMode.Create))
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
        using (var package = new ExcelPackage(new FileInfo(FilePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
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
            foreach (var rowData in DATA)
            {
                var Personal = rowData[0];
                var TransactionCode = rowData[1];

                var userId = await _userBillingRep.ExistBillingForPersonalId(Personal);
                if (userId == 0)
                {
                    ErrorList error = new()
                    {
                        Caption = Personal,
                        ErrorMessage = "کاربر در سامانه یافت نشد"
                    };
                    errors.Add(error);
                }


                if (!await _walletTransactionRep.ExistTransactionForUser(userId, TransactionCode))
                {
                    ErrorList error = new()
                    {
                        Caption = TransactionCode,
                        ErrorMessage = "کد تراکتش برای کاربر یافت نشد"
                    };
                    errors.Add(error);
                }
            }

            if (errors.Count > 0)
            {
                Response.IsSuccessed = false;
                Response.Object = errors;
                return Response;


            }

                
            foreach (var rowData in DATA)
            {
                var Personal = rowData[0];
                var TransactionCode = rowData[1];


                var userId = await _userBillingRep.ExistBillingForPersonalId(Personal);
                var Transaction = await _walletTransactionRep.GetTransactionByTrackingCode(TransactionCode);

                var result =await _mediator.Send(new GhestPaymentQueryReq(Transaction.Id));
                    
                    
                if (!result.IsSuccessed)
                {
                    ErrorList error = new()
                    {
                        Caption = TransactionCode,
                        ErrorMessage = "خطایی رخ داده است.امکان تسویه تراکتش وجود ندارد"
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