using AutoMapper;

using Dapper;

using MediatR;
using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class GetSomeOfLastProductsByCateguryIdQueryHandler : IRequestHandler<GetSomeOfLastProductsByCateguryIdQueryReq, List<ProductListShowDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductQueries _productQueries;
    private readonly IProductStockPriceRepository _productDtockRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IProductGallleryRepository _GalleryRep;
    private readonly IMapper _mapper;
    private readonly string _connectionString;

    public GetSomeOfLastProductsByCateguryIdQueryHandler(IProductRepository productRep,
        IProductGallleryRepository GalleryRep,
        ICateguryRepository categuryRep,
        IProductStockPriceRepository productDtockRep,
        IMapper mapper, IProductQueries productQueries, IConfiguration configuration)
    {
        _productRep = productRep;
        _categuryRep = categuryRep;
        _GalleryRep = GalleryRep;
        _mapper = mapper;
        _productDtockRep = productDtockRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<List<ProductListShowDto>> Handle(GetSomeOfLastProductsByCateguryIdQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductListShowDto> Result = new();

        var categury = await _categuryRep.GetAsync(request.CateguryId);
        var list = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetLastListByGroup, new { @categuryLatinName = categury.LatinGroupTitle }).ToList());

        foreach (var item in list)
        {
            var Dto = _mapper.Map<ProductListShowDto>(item);
            var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = item.ProductId }).FirstOrDefault());
            if (image != null)
            {
                Dto.Image = image.ImageName;
            }
                
            Dto.CateguryLatinName = categury.LatinGroupTitle;
            Dto.ProductId=item.ProductId;

            if (item.TypeId == 2)
            {
                    
                var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation, new {@parentId=item.ProductId}).ToList());
                if(variations.Count > 0)
                {
                    var variation = variations.FirstOrDefault();
                    Dto.ProductStockPriceId = variation.Id;
                    Dto.Quantity = variation.Quantity;
                    Dto.SalePrice = variation.SalePrice;
                    Dto.DiscountPrice = variation.DiscountPrice;
                }
                else
                {
                    Dto.ProductStockPriceId = item.Id;
                }
            }
            else
            {
                Dto.ProductStockPriceId = item.Id;
            }

            Result.Add(Dto);
        }


        return Result;
    }
}