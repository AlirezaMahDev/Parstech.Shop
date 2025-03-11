using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.Categury.Queries
{
    public interface ICateguryQueries
    {
        string GetBlankCateguries { get; }
        string GetParrentCateguries { get; }
        string GetChalidOfParrentCateguries { get; }
        string GetChalidOfChildCateguries { get; }
        string GetMenuParrentCateguries { get; }
        string GetMenuChalidOfParrentCateguries { get; }
        string GetMenuChalidOfChildCateguries { get; }
    }
}
