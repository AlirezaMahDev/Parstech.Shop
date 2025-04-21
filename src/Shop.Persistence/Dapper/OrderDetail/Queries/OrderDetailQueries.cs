using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.OrderDetail.Queries
{
    public class OrderDetailQueries: IOrderDetailQueries
    {
        public string GetOrderDetailsOfSaleStore => "SELECT dbo.OrderDetail.Count,dbo.OrderDetail.DetailSum,dbo.OrderDetail.Discount,dbo.OrderDetail.Price,dbo.OrderDetail.Tax,dbo.OrderDetail.Total,dbo.Orders.OrderCode,dbo.Orders.CreateDate,dbo.ProductStockPrice.Id As ProductStockPriceId,dbo.Product.Name,dbo.ProductStockPrice.StoreId FROM dbo.OrderDetail INNER JOIN dbo.Orders on .dbo.OrderDetail.OrderId=dbo.Orders.OrderId INNER JOIN dbo.ProductStockPrice on .dbo.OrderDetail.ProductStockPriceId=dbo.ProductStockPrice.Id INNER JOIN dbo.Product on .dbo.ProductStockPrice.ProductId=dbo.Product.Id WHERE dbo.ProductStockPrice.StoreId=@storeId";
        public string GetOrderDetailsOfAllSaleStore => "SELECT dbo.OrderDetail.Count,dbo.OrderDetail.DetailSum,dbo.OrderDetail.Discount,dbo.OrderDetail.Price,dbo.OrderDetail.Tax,dbo.OrderDetail.Total,dbo.Orders.OrderId,dbo.Orders.OrderCode,dbo.Orders.CreateDate,dbo.ProductStockPrice.Id As ProductStockPriceId,dbo.Product.Name,dbo.ProductStockPrice.StoreId ,dbo.UserStore.StoreName FROM dbo.OrderDetail INNER JOIN dbo.Orders on .dbo.OrderDetail.OrderId=dbo.Orders.OrderId INNER JOIN dbo.ProductStockPrice on .dbo.OrderDetail.ProductStockPriceId=dbo.ProductStockPrice.Id INNER JOIN dbo.Product on .dbo.ProductStockPrice.ProductId=dbo.Product.Id INNER JOIN dbo.UserStore on dbo.ProductStockPrice.StoreId=dbo.UserStore.Id";

        string IOrderDetailQueries.GetOrderDetailOfProductStockPriceId => "select* from OrderDetail where ProductStockPriceId=@productId";
    }
}
