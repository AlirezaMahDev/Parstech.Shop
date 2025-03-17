using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class
    GetSomeOfLastProductsByCateguryIdQueryHandler : IRequestHandler<GetSomeOfLastProductsByCateguryIdQueryReq,
    List<ProductListShowDto>>
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
        IMapper mapper,
        IProductQueries productQueries,
        IConfiguration configuration)
    {
        _productRep = productRep;
        _categuryRep = categuryRep;
        _GalleryRep = GalleryRep;
        _mapper = mapper;
        _productDtockRep = productDtockRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<ProductListShowDto>> Handle(GetSomeOfLastProductsByCateguryIdQueryReq request,
        CancellationToken cancellationToken)
    {
        List<ProductListShowDto> Result = new();

        Shared.Models.Categury? categury = await _categuryRep.GetAsync(request.CateguryId);
        List<DapperProductDto> list = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<DapperProductDto>(_productQueries.GetLastListByGroup,
                    new { @categuryLatinName = categury.LatinGroupTitle })
                .ToList());

        foreach (DapperProductDto item in list)
        {
            ProductListShowDto? Dto = _mapper.Map<ProductListShowDto>(item);
            Shared.Models.ProductGallery image = DapperHelper.ExecuteCommand<Shared.Models.ProductGallery>(
                _connectionString,
                conn => conn
                    .Query<Shared.Models.ProductGallery>(_productQueries.GetMainImage,
                        new { @productId = item.ProductId })
                    .FirstOrDefault());
            if (image != null)
            {
                Dto.Image = image.ImageName;
            }

            Dto.CateguryLatinName = categury.LatinGroupTitle;
            Dto.ProductId = item.ProductId;

            if (item.TypeId == 2)
            {
                List<DapperProductDto> variations = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation,
                            new { @parentId = item.ProductId })
                        .ToList());
                if (variations.Count > 0)
                {
                    DapperProductDto? variation = variations.FirstOrDefault();
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