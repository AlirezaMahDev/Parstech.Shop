using Shop.Application.Dapper.Product.Commands;
using Shop.Application.Dapper.ProductStockPrice.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.ProductStockPrice.Commands
{

    public class ProductStockPriceCommand : IProductStockPriceCommand
    {
        public string GetProductStockPriceById => "Select * From ProductStockPrice Where Id=@Id";
    }
}
