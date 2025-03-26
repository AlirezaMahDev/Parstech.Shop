using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Api.Handlers.Queries;

public class FixDuplicateProductsByCodeQueryHandler : IRequestHandler<FixDuplicateProductsByCodeQueryReq, ResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IProductQueries _productQueries;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly string _connectionString;
    public FixDuplicateProductsByCodeQueryHandler(IProductQueries productQueries
        ,IConfiguration configuration,
        IProductRepository productRep,
        IMediator mediator,
        IProductStockPriceRepository productStockRep)
    {
        _mediator= mediator;
        _productQueries= productQueries;
        _productRep= productRep;
        _productStockRep= productStockRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }



    public class ListItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public List<DapperProductDto> Items { get; set; }
    }

    public List<ListItem> ListItems { get; set; }=new();
    public List<ListItem> FinalListItems { get; set; }=new();

    public async Task<ResponseDto> Handle(FixDuplicateProductsByCodeQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        var AllList = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());
        foreach(var item in AllList)
        {
            if (item.Code != null&&item.Code!="")
            {
                bool containsLetter = item.Code.Any(char.IsLetter);
                if (containsLetter)
                {
                    string resultString = new(item.Code.Where(c => !char.IsLetter(c)).ToArray());
                    var current=ListItems.FirstOrDefault(x => x.Code == resultString);
                    if (current != null)
                    {
                        current.Items.Add(item);
                    }
                    else
                    {
                        ListItem newitem = new();
                        newitem.Id = item.Id;
                        newitem.Code = resultString;
                        newitem.Items = new();

                        ListItems.Add(newitem);
                    }
                }

                else
                {
                    ListItem newitem = new();
                    newitem.Id = item.Id;
                    newitem.Code = item.Code;
                    newitem.Items = new();

                    ListItems.Add(newitem);
                }
            }

        }

        foreach(var item in ListItems.Where(u=>u.Items.Count() > 0))
        {
            FinalListItems.Add(item);
        }
        foreach (var item in FinalListItems)
        {
            var parrent=await _productRep.GetAsync(item.Id);
            foreach(var item2 in item.Items)
            {
                var stocks=await _productStockRep.GetAllByProductId(item2.Id);
                foreach(var stock in stocks)
                {
                    stock.ProductId = parrent.Id;
                    await _productStockRep.UpdateAsync(stock);
                    await _mediator.Send(new ProductDeleteQueryReq(item2.Id));
                }
            }
        }
        response.Object = FinalListItems;



        return response;
    }
}