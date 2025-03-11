using ExcelDataReader;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.Excel.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Validators.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Excel.Handlers.Queries
{

    public class UpdateUserWalletsByExcelQueryHandler : IRequestHandler<UpdateUserWalletsByExcelQueryReq, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IWalletRepository _walletRep;
        private readonly IUserRepository _userRep;
        public UpdateUserWalletsByExcelQueryHandler(IMediator mediator, IWalletRepository walletRep, IUserRepository userRep)
        {
            _mediator = mediator;
            _walletRep = walletRep;
            _userRep = userRep;
        }
        public class res
        {

            public string mobile { get; set; }
            public string code { get; set; }
            public string credit { get; set; }
        }
        public async Task<Unit> Handle(UpdateUserWalletsByExcelQueryReq request, CancellationToken cancellationToken)
        {
            List<res> list = new List<res>();
            var filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Shared\Files"}" + "\\" + request.fileName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            res m = new res()
                            {

                                code = reader.GetValue(0).ToString(),
                                mobile = reader.GetValue(1).ToString(),
                                credit = reader.GetValue(2).ToString(),
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
            foreach (var item in list.Skip(1))
            {
                var userName = $"0{item.mobile}";
                var user = await _userRep.GetUserByUserName(userName);
                if (user != null)
                {
                    var wallet = await _walletRep.GetWalletByUserId(user.Id);
					var y = Convert.ToDouble(item.credit);
					int itemCredit = (int)Math.Ceiling(y);
					//int itemCredit = Convert.ToInt32(item.credit);
                    int typeTransaction = 0;
                    var typePrice = 0;
                    int WalletPrice = Convert.ToInt32(wallet.OrgCredit);

                    if (itemCredit > wallet.OrgCredit)
                    {
                        typeTransaction = 1;
                        typePrice = itemCredit - WalletPrice;
                        #region Validator
                        WalletTransactionDto Transaction = new WalletTransactionDto();

                        Transaction.WalletId = wallet.WalletId;
                        Transaction.Price = typePrice;
                        Transaction.TypeId = typeTransaction;
                        Transaction.Type = "OrgCredit";
                        Transaction.Description = "استعلام مانده وام سرمایه انسانی";
                        var result = await _mediator.Send(new CreateWalletTransactionCommandReq(Transaction, true));

                        #endregion
                    }
                    if (itemCredit < wallet.OrgCredit)
                    {
                        typeTransaction = 2;

                        typePrice = WalletPrice - itemCredit;
                        #region Validator
                        WalletTransactionDto Transaction = new WalletTransactionDto();

                        Transaction.WalletId = wallet.WalletId;
                        Transaction.Price = typePrice;
                        Transaction.TypeId = typeTransaction;
                        Transaction.Type = "OrgCredit";
                        Transaction.Description = "استعلام مانده وام سرمایه انسانی";
                        var result = await _mediator.Send(new CreateWalletTransactionCommandReq(Transaction, true));

                        #endregion
                    }





                }
            }
            return Unit.Value;
        }
    }
}
