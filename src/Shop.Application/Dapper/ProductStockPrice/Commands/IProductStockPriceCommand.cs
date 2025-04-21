using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.ProductStockPrice.Commands
{
    public interface IProductStockPriceCommand
    {
        string GetProductStockPriceById { get; }
    }
}
