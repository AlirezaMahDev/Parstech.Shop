using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.WalletTransaction.Queries
{
    public interface ITransactionQueries
    {
        string GetAllTransaction { get; }
        string GetActiveCredit { get; }
    }
}
