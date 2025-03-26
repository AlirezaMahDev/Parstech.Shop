using System.Globalization;

using AutoMapper;

using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

public class OrderPagingQueryHandler : IRequestHandler<OrderPagingQueryReq, PagingDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IMapper _mapper;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IUserRepository _userRep;
    private readonly IOrderStatusRepository _orderStatusRep;
    private readonly string _connectionString;

    public OrderPagingQueryHandler(IOrderRepository orderRepository,
        IMapper mapper, IUserBillingRepository userBillingRep,
        IOrderStatusRepository orderStatusRep,
        IConfiguration configuration, IOrderPayRepository orderPayRep, IUserStoreRepository userStoreRep, IUserRepository userRep)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userBillingRep = userBillingRep;
        _orderStatusRep = orderStatusRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _orderPayRep = orderPayRep;
        _userStoreRep = userStoreRep;
        _userRep = userRep;
    }
    public async Task<PagingDto> Handle(OrderPagingQueryReq request, CancellationToken cancellationToken)
    {
        var orders = new List<OrderDto>();
            
        var storeQuery = "";
        var payQuery = "";
        var statusQuery = "";
        var fromDatesQuery = "";
        var toDatesQuery = "";
        var filterQuery = "";
        var customerQuery = "";

        var skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;
        var pagingQuery = $"OFFSET {skip} ROWS FETCH NEXT {request.Parameter.TakePage} ROWS ONLY";

        var countQuery = "SELECT COUNT(*) as Count from dbo.Orders";

        if (request.Parameter.store != null)
        {
            var user = await _userRep.GetUserByUserName(request.Parameter.store);
            var store = await _userStoreRep.GetStoreOfUser(user.Id);
            storeQuery = $"AND dbo.UserStore.Id={store.Id}";
                
        }

        if (request.Parameter.PayType != 0)
        {
            payQuery = $"AND dbo.OrderPay.PayTypeId={request.Parameter.PayType}";

            if(request.Parameter.PayType == 999)
            {
                payQuery = $"AND dbo.OrderPay.OrderId IS NULL";
            }
        }
        if (request.Parameter.Status != 0)
        {
            statusQuery = $"AND dbo.OrderStatus.StatusId={request.Parameter.Status}";
            if (request.Parameter.Status == 999)
            {
                statusQuery = $"AND dbo.Status.StatusName IS NULL";
            }
        }
            
        if (request.Parameter.UserId != 0)
        {
            customerQuery = $"And dbo.Orders.UserId={request.Parameter.UserId}";
        }

            

        if (request.Parameter.FromDate != null)
        {
            request.Parameter.FromDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.Parameter.FromDate);

            string[] std = request.Parameter.FromDate.Split('/');
            var az = new DateTime(int.Parse(std[0]),
                int.Parse(std[1]),
                int.Parse(std[2]),
                new PersianCalendar()
            );
            fromDatesQuery = $"AND dbo.Orders.CreateDate >= '{az.ToShortDateString()}'";
        }
        if (request.Parameter.ToDate != null)
        {
            request.Parameter.ToDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.Parameter.ToDate);
            string[] std = request.Parameter.ToDate.Split('/');
            var ta = new DateTime(int.Parse(std[0]),
                int.Parse(std[1]),
                int.Parse(std[2]),
                new PersianCalendar()
            );
            toDatesQuery = $"AND dbo.Orders.CreateDate <= '{ta.ToShortDateString()}'";
        }

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {

            filterQuery = $"AND dbo.Orders.OrderCode='{request.Parameter.Filter}'";
            pagingQuery = "";
        }

        var query = $"SELECT dbo.Orders.OrderId,dbo.Orders.OrderCode,dbo.Status.StatusName,dbo.UserBilling.FirstName,dbo.UserBilling.LastName,dbo.Orders.Total, dbo.PayType.TypeName,dbo.Orders.CreateDate FROM dbo.Orders LEFT JOIN dbo.[User] ON dbo.Orders.UserId = dbo.[User].Id LEFT JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId LEFT JOIN dbo.OrderStatus ON dbo.Orders.OrderId = dbo.OrderStatus.OrderId LEFT JOIN dbo.OrderPay ON dbo.OrderPay.OrderId = dbo.Orders.OrderId LEFT JOIN dbo.PayType ON dbo.OrderPay.PayTypeId = dbo.PayType.Id LEFT JOIN dbo.Status ON dbo.OrderStatus.StatusId = dbo.Status.Id LEFT JOIN dbo.OrderDetail ON dbo.Orders.OrderId = dbo.OrderDetail.OrderId LEFT JOIN dbo.ProductStockPrice ON dbo.OrderDetail.ProductStockPriceId = dbo.ProductStockPrice.Id LEFT JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id where (dbo.OrderStatus.IsActive=1 OR dbo.OrderStatus.OrderId is null) {storeQuery} {statusQuery} {payQuery} {customerQuery} {fromDatesQuery} {toDatesQuery} {filterQuery} ORDER BY dbo.Orders.CreateDate Desc {pagingQuery}";
        orders = DapperHelper.ExecuteCommand<List<OrderDto>>(_connectionString, conn => conn.Query<OrderDto>(query).ToList());

        // نمایش تکرارها
        var duplicates = orders
            .GroupBy(x => x.OrderId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
        foreach (var item in duplicates)
        {
            var od = orders.Where(u => u.OrderId == item).Skip(1).ToList();
            foreach (var o in od)
            {
                orders.Remove(o);
            }
        }
            
        foreach(var item in orders)
        {
            item.CreateDateShamsi = item.CreateDate.ToShamsi();
            if (item.StatusName == null)
            {
                item.StatusName = "سبد خرید";
            }
            if (item.TypeName == null)
            {
                item.TypeName = "ثبت نشده";
            }
                
        }
        IQueryable<OrderDto> result = orders.AsQueryable();
        PagingDto response = new();

        response.CurrentPage = request.Parameter.CurrentPage;


            
        var countDto = DapperHelper.ExecuteCommand<CountDto>(_connectionString, conn => conn.Query<CountDto>(countQuery).FirstOrDefault());

        response.PageCount = countDto.Count / request.Parameter.TakePage;
        if((response.PageCount* request.Parameter.TakePage) < countDto.Count)
        {
            response.PageCount++;
        }
        response.List = result.ToArray();

        return response;

    }
}