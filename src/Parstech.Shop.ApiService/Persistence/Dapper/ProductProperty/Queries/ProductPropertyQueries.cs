using Shop.Application.Dapper.ProductProperty.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.ProductProperty.Queries
{
    public class ProductPropertyQueries : IProductPropertyQueries
    {
        public string GetCommonByTwoProductId => "SELECT PropertyId FROM ProductProperty AS C WHERE C.ProductId=@firstProductId and C.PropertyId IN (SELECT  PropertyId  FROM ProductProperty AS E WHERE (E.ProductId=@secoundProductId) );";
        public string GetOneByProductIdAndPropertyId => "SELECT dbo.Product.Name, dbo.Product.Code, dbo.ProductProperty.PropertyId, dbo.Property.Caption, dbo.ProductProperty.Value FROM dbo.Product INNER JOIN dbo.ProductProperty ON dbo.Product.Id = dbo.ProductProperty.ProductId INNER JOIN dbo.Property ON dbo.ProductProperty.PropertyId = dbo.Property.Id INNER JOIN dbo.PropertyCategury ON dbo.Property.PropertyCateguryId = dbo.PropertyCategury.Id where productId=@productId AND propertyId=@propertyId Order By PropertyId asc";
        public string GeAllByProductId => "SELECT dbo.Product.Id,dbo.Product.Name, dbo.Product.Code, dbo.ProductProperty.PropertyId, dbo.Property.Caption, dbo.ProductProperty.Value FROM dbo.Product INNER JOIN dbo.ProductProperty ON dbo.Product.Id = dbo.ProductProperty.ProductId INNER JOIN dbo.Property ON dbo.ProductProperty.PropertyId = dbo.Property.Id INNER JOIN dbo.PropertyCategury ON dbo.Property.PropertyCateguryId = dbo.PropertyCategury.Id where productId=@productId Order By PropertyId asc";
        public string GeAllCompareUserProductsByUserId => "SELECT* FROM UserProduct WHERE UserId=@userId AND Type='Compare'";
        public string GeAllFavoriteUserProductsByUserId => "SELECT* FROM UserProduct WHERE UserId=@userId AND Type='Favorite'";
    }
}
