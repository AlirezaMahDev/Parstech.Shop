using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Handlers.Queries;

public class FixDuplicateProductsByCodeQueryHandler : IRequestHandler<FixDuplicateProductsByCodeQueryReq, ResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IProductQueries _productQueries;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly string _connectionString;

    public FixDuplicateProductsByCodeQueryHandler(IProductQueries productQueries,
        IConfiguration configuration,
        IProductRepository productRep,
        IMediator mediator,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _productQueries = productQueries;
        _productRep = productRep;
        _productStockRep = productStockRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public class ListItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public List<DapperProductDto> Items { get; set; }
    }

    public List<ListItem> ListItems { get; set; } = new();
    public List<ListItem> FinalListItems { get; set; } = new();

    public async Task<ResponseDto> Handle(FixDuplicateProductsByCodeQueryReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        List<DapperProductDto> AllList = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());
        foreach (DapperProductDto item in AllList)
        {
            if (item.Code != null && item.Code != "")
            {
                bool containsLetter = item.Code.Any(char.IsLetter);
                if (containsLetter)
                {
                    string resultString = new(item.Code.Where(c => !char.IsLetter(c)).ToArray());
                    ListItem? current = ListItems.FirstOrDefault(x => x.Code == resultString);
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

        foreach (ListItem item in ListItems.Where(u => u.Items.Count() > 0))
        {
            FinalListItems.Add(item);
        }

        foreach (ListItem item in FinalListItems)
        {
            Shared.Models.Product? parrent = await _productRep.GetAsync(item.Id);
            foreach (DapperProductDto item2 in item.Items)
            {
                List<Shared.Models.ProductStockPrice> stocks = await _productStockRep.GetAllByProductId(item2.Id);
                foreach (Shared.Models.ProductStockPrice stock in stocks)
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