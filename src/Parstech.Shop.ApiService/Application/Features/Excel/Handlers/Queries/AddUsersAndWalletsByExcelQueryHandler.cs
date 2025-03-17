using ExcelDataReader;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Validators.WalletTransaction;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Handlers.Queries;

public class AddUsersAndWalletsByExcelQueryHandler : IRequestHandler<AddUsersAndWalletsByExcelQueryReq, Unit>
{
    private readonly IMediator _mediator;
    private readonly IWalletRepository _walletRep;
    private readonly IUserRepository _userRep;

    public AddUsersAndWalletsByExcelQueryHandler(IMediator mediator,
        IWalletRepository walletRep,
        IUserRepository userRep)
    {
        _mediator = mediator;
        _walletRep = walletRep;
        _userRep = userRep;
    }

    public class res
    {
        public string branch { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string mobile { get; set; }
        public string code { get; set; }
        public string credit { get; set; }
    }

    public async Task<Unit> Handle(AddUsersAndWalletsByExcelQueryReq request, CancellationToken cancellationToken)
    {
        List<res> list = new();
        string filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Shared\Files"}" + "\\" + request.fileName;
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using (FileStream stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
        {
            using (IExcelDataReader? reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    try
                    {
                        res m = new()
                        {
                            branch = reader.GetValue(0).ToString(),
                            name = reader.GetValue(1).ToString(),
                            family = reader.GetValue(2).ToString(),
                            code = reader.GetValue(3).ToString(),
                            mobile = reader.GetValue(4).ToString(),
                            credit = reader.GetValue(5).ToString()
                        };
                        list.Add(m);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
        }

        foreach (res item in list.Skip(1))
        {
            #region CreateUser

            UserRegisterDto userRegisterDto = new();
            userRegisterDto.Address = item.branch;
            userRegisterDto.RoleName = "BankCustomer";
            userRegisterDto.UserName = $"0{item.mobile}";
            userRegisterDto.Mobile = $"0{item.mobile}";
            userRegisterDto.EconomicCode = item.code;
            userRegisterDto.FirstName = item.name;
            userRegisterDto.LastName = item.family;
            userRegisterDto.State = "تهران";
            userRegisterDto.City = "تهران";
            var res = await _mediator.Send(new UserRegisterQueryReq(userRegisterDto));

            Shared.Models.User? user = await _userRep.GetUserByUserName(userRegisterDto.UserName);

            #endregion

            if (res.IsSuccessed && user != null)
            {
                Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(user.Id);

                #region Validator

                WalletTransactionDto Transaction = new();
                Transaction.WalletId = wallet.WalletId;
                Transaction.InputPrice = item.credit;
                Transaction.Month = 6;
                Transaction.Persent = 0;
                Transaction.TypeId = 1;
                Transaction.Type = "OrgCredit";
                var validator = new TransactionValidator();
                var valid = validator.Validate(Transaction);
                if (valid.IsValid)
                {
                    int inputPrice = int.Parse(item.credit);
                    Transaction.Price = inputPrice / 10;
                    Transaction.Start = false;
                    Transaction.Description = "ثبت تسهیلات جدید";
                    var result = await _mediator.Send(new CreateWalletTransactionCommandReq(Transaction, true));
                }

                #endregion
            }
        }


        return Unit.Value;
    }
}