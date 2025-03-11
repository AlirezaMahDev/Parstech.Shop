using ExcelDataReader;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Excel.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shop.Application.Features.Excel.Handlers.Queries.FixRezerveProductQueryHandler;

namespace Shop.Application.Features.Excel.Handlers.Queries
{
    public class EditCateguriesOfProductQueryHandler : IRequestHandler<EditCateguriesOfProductQueryReq, Unit>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IProductRepository _productRep;
        private readonly ICateguryRepository _categuryRep;
        public EditCateguriesOfProductQueryHandler(IProductCateguryRepository productCateguryRep,
            IProductRepository productRep, ICateguryRepository categuryRep)
        {
            _productCateguryRep = productCateguryRep;
            _productRep = productRep;
            _categuryRep = categuryRep;
        }

        public class pcRes
        {
            public string name { get; set; }
            public string cat1 { get; set; }
            public string cat2 { get; set; }
            public string cat3 { get; set; }
        }

        public async Task<Unit> Handle(EditCateguriesOfProductQueryReq request, CancellationToken cancellationToken)
        {
            List<pcRes> list = new List<pcRes>();
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
                            pcRes m = new pcRes()
                            {
                                name = reader.GetValue(0).ToString(),
                                cat1 = reader.GetValue(1).ToString(),
                                cat2 = reader.GetValue(2).ToString(),
                                cat3 = reader.GetValue(3).ToString(),

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
            foreach (var item in list.Skip(1))
            {
                
                var product = await _productRep.GetProductsByName(item.name);
                var cat1 = await _categuryRep.GetCateguryByName(item.cat1);
                var cat2 = await _categuryRep.GetCateguryByName(item.cat2);
                var cat3 = await _categuryRep.GetCateguryByName(item.cat3);
                if (product != null)
                {
                    if (cat1 != null)
                    {
                        var ProductHavecategury = await _productCateguryRep.ProductHaveCategury(product.Id, cat1.GroupId);
                        if (!ProductHavecategury)
                        {
                            Domain.Models.ProductCategury newProductCategury = new Domain.Models.ProductCategury()
                            {
                                ProductId = product.Id,
                                CateguryId = cat1.GroupId
                            };
                            await _productCateguryRep.AddAsync(newProductCategury);
                        }

                    }
                    if (cat2 != null)
                    {
                        var ProductHavecategury = await _productCateguryRep.ProductHaveCategury(product.Id, cat2.GroupId);
                        if (!ProductHavecategury)
                        {
                            Domain.Models.ProductCategury newProductCategury = new Domain.Models.ProductCategury()
                            {
                                ProductId = product.Id,
                                CateguryId = cat2.GroupId
                            };
                            await _productCateguryRep.AddAsync(newProductCategury);
                        }

                    }
                    if (cat3 != null)
                    {
                        var ProductHavecategury = await _productCateguryRep.ProductHaveCategury(product.Id, cat3.GroupId);
                        if (!ProductHavecategury)
                        {
                            Domain.Models.ProductCategury newProductCategury = new Domain.Models.ProductCategury()
                            {
                                ProductId = product.Id,
                                CateguryId = cat3.GroupId
                            };
                            await _productCateguryRep.AddAsync(newProductCategury);
                        }

                    }
                }
                


            }



            return Unit.Value;
        }
    }
}
