using ExcelDataReader;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Handlers.Queries;

public class FixRezerveProductQueryHandler : IRequestHandler<FixRezerveProductQueryReq, Unit>
{
    private IProductRepository _productRepository;
    private IProductStockPriceRepository _productStockPriceRepository;

    public FixRezerveProductQueryHandler(IProductRepository productRepository,
        IProductStockPriceRepository productStockPriceRepository)
    {
        _productRepository = productRepository;
        _productStockPriceRepository = productStockPriceRepository;
    }

    public class res
    {
        public string id { get; set; }
    }

    public async Task<Unit> Handle(FixRezerveProductQueryReq request, CancellationToken cancellationToken)
    {
        List<res> list = new();
        string filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Shared\Files"}" + "\\" + request.fileName;
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using (FileStream stream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
        {
            using (IExcelDataReader? reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    try
                    {
                        res m = new() { id = reader.GetValue(0).ToString() };
                        list.Add(m);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
        }

        foreach (res item in list)
        {
            Domain.Models.Product product = await _productRepository.GetProductByCode(item.id);
            if (product.Id != 0)
            {
                int pstocke = await _productStockPriceRepository.GetFirstProductStockPriceIdFromProductId(product.Id);
                Domain.Models.ProductStockPrice? ps = await _productStockPriceRepository.GetAsync(pstocke);
                ps.RepId = 1;
                ps.StoreId = 1;
                await _productStockPriceRepository.UpdateAsync(ps);
            }
        }


        return Unit.Value;
    }
}