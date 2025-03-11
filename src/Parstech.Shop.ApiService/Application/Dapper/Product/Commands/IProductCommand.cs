using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.Product.Commands
{
    public interface IProductCommand
    {
        string GetProductById { get; }
    }
}
