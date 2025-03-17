using ExcelDataReader;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class AddProductsByExcelQueryHandler : IRequestHandler<AddProductsByExcelQueryReq, Unit>
{
    private readonly IProductRepository _productRep;
    private readonly IMediator _mediator;

    public AddProductsByExcelQueryHandler(IProductRepository productRep,
        IMediator mediator)
    {
        _productRep = productRep;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AddProductsByExcelQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductExcelDto> list = new();
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
                        ProductExcelDto m = new()
                        {
                            Id = reader.GetValue(0).ToString(),
                            Type = reader.GetValue(1).ToString(),
                            Name = reader.GetValue(2).ToString(),
                            Parent = reader.GetValue(3).ToString(),
                            Description = reader.GetValue(4).ToString(),
                            brandId = reader.GetValue(5).ToString()
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

        foreach (ProductExcelDto item in list.Skip(1))
        {
            ProductDto product = new()
            {
                Name = item.Name,
                Description = item.Description,
                ShortDescription = "-",
                Code = item.Id,
                StoreId = 1,
                TaxId = 1,
                BrandId = int.Parse(item.brandId)
            };
            switch (item.Type)
            {
                case "simple":
                    product.TypeId = 1;
                    break;
                case "variable":
                    product.TypeId = 2;
                    break;
                case "variation":
                    product.TypeId = 3;
                    if (item.Parent != "0")
                    {
                        Domain.Models.Product parent = await _productRep.GetProductByCode(item.Parent);
                        if (parent.Id != 0)
                        {
                            product.ParentId = parent.Id;
                        }
                    }

                    break;
            }

            await _mediator.Send(new ProductCreateCommandReq(product));
        }


        return Unit.Value;
    }
}