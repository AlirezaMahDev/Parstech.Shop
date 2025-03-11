using Shop.Application.Dapper.Product.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.Product.Commands
{
    public class ProductCommand: IProductCommand
    {

        public string GetProductById => "Select * From Product Where Id=@Id";
    }
}
