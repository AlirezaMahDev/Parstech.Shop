using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.ProductStockPrice.Queries
{
    public interface IproductStockPriceQuery
    {
        string GetDiscountProductSTockPricePaging(int skip);
    }
}
