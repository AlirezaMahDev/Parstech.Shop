using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.ProductProperty.Queries
{
    public interface IProductPropertyQueries
    {
        public string GetCommonByTwoProductId { get; }
        public string GetOneByProductIdAndPropertyId { get; }
        public string GeAllByProductId { get; }
        public string GeAllCompareUserProductsByUserId { get; }
        public string GeAllFavoriteUserProductsByUserId { get; }

    }
}
