using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.Rahkaran.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Application.Features.Rahkaran.Handlers.Queries
{
    public class RahakaranAllQueryHandler : IRequestHandler<RahakaranAllQueryReq, RahkaranAllDto>
    {
        private readonly IMediator _mediator;
        private readonly string _connectionString;
        public RahakaranAllQueryHandler(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<RahkaranAllDto> Handle(RahakaranAllQueryReq request, CancellationToken cancellationToken)
        {
            RahkaranAllDto result = new RahkaranAllDto();
            var order = await _mediator.Send(new OrderReadCommandReq(request.orderId));
            var orderdetails = await _mediator.Send(new OrderDetailsOfOrderQueryReq(request.orderId));

            var orderQuery = $"select dbo.Orders.OrderId,dbo.Orders.OrderCode,dbo.RahkaranOrder.RahkaranPishNumber,dbo.RahkaranOrder.RahakaranFactorNumber,dbo.RahkaranOrder.RahakaranFactorSerial from dbo.Orders left join dbo.RahkaranOrder on dbo.Orders.OrderId=dbo.RahkaranOrder.OrderId where dbo.Orders.OrderId={order.OrderId}";
            result.order = DapperHelper.ExecuteCommand<RahkaranOrderDto>(_connectionString, conn => conn.Query<RahkaranOrderDto>(orderQuery).FirstOrDefault());

            var userQuery = $"select dbo.[User].Id,dbo.[User].UserName,dbo.UserBilling.FirstName,dbo.UserBilling.LastName,dbo.UserBilling.NationalCode,dbo.UserBilling.EconomicCode,dbo.RahkaranUser.RahkaranUserId from dbo.[User] inner join dbo.UserBilling on dbo.[User].Id=dbo.UserBilling.UserId left join dbo.RahkaranUser on dbo.[User].Id=dbo.RahkaranUser.UserId where dbo.[User].Id={order.UserId}";
            result.customer = DapperHelper.ExecuteCommand<RahkaranUserDto>(_connectionString, conn => conn.Query<RahkaranUserDto>(userQuery).FirstOrDefault());

            

            List<RahkaranProductDto> products = new List<RahkaranProductDto>();
            foreach (var item in orderdetails)
            {
                var productQuery = $"select dbo.OrderDetail.Id as DetailId,dbo.OrderDetail.Count,dbo.OrderDetail.Total as Price,dbo.ProductStockPrice.Id as StockId, dbo.Product.Name,dbo.Product.VariationName,dbo.Product.Code,dbo.Product.Id as ProductId,dbo.RahkaranProduct.RahkaranProductId,dbo.RahkaranProduct.RahkaranUnitId from dbo.OrderDetail inner join dbo.ProductStockPrice on dbo.OrderDetail.ProductStockPriceId =dbo.ProductStockPrice.Id inner join dbo.Product on dbo.ProductStockPrice.ProductId=dbo.Product.Id left join dbo.RahkaranProduct on dbo.Product.Id=dbo.RahkaranProduct.ProductId where dbo.OrderDetail.Id={item.Id}";
                var product = DapperHelper.ExecuteCommand<RahkaranProductDto>(_connectionString, conn => conn.Query<RahkaranProductDto>(productQuery).FirstOrDefault());
                product.Price = product.Price * 10;
                products.Add(product);
            }
            result.products = products;
            return result;
        }
    }
}
