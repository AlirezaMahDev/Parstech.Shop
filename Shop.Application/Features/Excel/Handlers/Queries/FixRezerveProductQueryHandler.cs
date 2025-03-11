using ExcelDataReader;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Excel.Requests.Queries;
using Shop.Application.Features.Product.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Excel.Handlers.Queries
{
    public class FixRezerveProductQueryHandler : IRequestHandler<FixRezerveProductQueryReq, Unit>
    {
        private IProductRepository _productRepository;
        private IProductStockPriceRepository _productStockPriceRepository;
        public FixRezerveProductQueryHandler(IProductRepository productRepository,IProductStockPriceRepository productStockPriceRepository)
        {
            _productRepository = productRepository;
            _productStockPriceRepository= productStockPriceRepository;
        }
        public class res
        {
            public string id { get; set; }
        }
        public async Task<Unit> Handle(FixRezerveProductQueryReq request, CancellationToken cancellationToken)
        {
            List<res> list = new List<res>();
            var filename = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Shared\Files"}" + "\\" + request.fileName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            res m = new res()
                            {
                                id = reader.GetValue(0).ToString(),
                                
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
            foreach (var item in list)
            {
                var product =await _productRepository.GetProductByCode(item.id);
                if (product.Id!=0)
                {
                    var pstocke = await _productStockPriceRepository.GetFirstProductStockPriceIdFromProductId(product.Id);
                    var ps = await _productStockPriceRepository.GetAsync(pstocke);
                    ps.RepId = 1;
                    ps.StoreId = 1;
                    await _productStockPriceRepository.UpdateAsync(ps);
                }
              
            }



            return Unit.Value;
        }
    }
}
