using ExcelDataReader;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Handlers.Queries;

public class FillCodeOfProductsByExcelQueryHandler : IRequestHandler<FillCodeOfProductsByExcelQueryReq, Unit>
{
    private IProductRepository _productRepository;
    private IProductStockPriceRepository _productStockPriceRepository;

    public FillCodeOfProductsByExcelQueryHandler(IProductRepository productRepository,
        IProductStockPriceRepository productStockPriceRepository)
    {
        _productRepository = productRepository;
        _productStockPriceRepository = productStockPriceRepository;
    }

    public class res
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public async Task<Unit> Handle(FillCodeOfProductsByExcelQueryReq request, CancellationToken cancellationToken)
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
                        res m = new()
                        {
                            id = reader.GetValue(0).ToString(),
                            name = reader.GetValue(1).ToString(),
                            code = reader.GetValue(2).ToString()
                        };
                        list.Add(m);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
        }

        foreach (res item in list.Skip(1))
        {
            //int id = int.Parse(item.id);
            Shared.Models.Product product = await _productRepository.GetProductsByName(item.name);
            if (product != null && item.code != null)
            {
                product.Code = item.code;
                await _productRepository.UpdateAsync(product);
            }
        }


        return Unit.Value;
    }
}