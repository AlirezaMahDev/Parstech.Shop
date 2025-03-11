using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.OrderDetail.Queries
{
    public interface IOrderDetailQueries
    {
        string GetOrderDetailOfProductStockPriceId { get; }
        string GetOrderDetailsOfSaleStore { get; }
        string GetOrderDetailsOfAllSaleStore { get; }
    }
}
