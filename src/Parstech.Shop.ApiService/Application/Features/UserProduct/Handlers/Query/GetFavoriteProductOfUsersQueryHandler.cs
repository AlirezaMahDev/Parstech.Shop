using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Dapper.ProductProperty.Queries;
using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Query;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Handlers.Query;

public class
    GetFavoriteProductOfUsersQueryHandler : IRequestHandler<GetFavoriteProductOfUsersQueryReq, List<FavoriteDto>>
{
    private readonly IProductPropertyQueries _productPropertyQueries;
    private readonly IProductQueries _productQueries;
    private readonly IConfiguration configuration;
    private readonly IUserRepository _userRep;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly string _ConnectionString;

    public GetFavoriteProductOfUsersQueryHandler(IProductPropertyQueries productPropertyQueries,
        IConfiguration configuration,
        IProductQueries productQueries,
        IUserRepository userRep,
        IProductRepository productRep,
        IProductStockPriceRepository productStockRep)
    {
        _productPropertyQueries = productPropertyQueries;
        _userRep = userRep;
        _productQueries = productQueries;
        _ConnectionString = configuration.GetConnectionString("DatabaseConnection");
        _productRep = productRep;
        _productStockRep = productStockRep;
    }

    public async Task<List<FavoriteDto>> Handle(GetFavoriteProductOfUsersQueryReq request,
        CancellationToken cancellationToken)
    {
        List<FavoriteDto> result = new();
        Shared.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        List<Shared.Models.UserProduct> userProduct = DapperHelper.ExecuteCommand(_ConnectionString,
            conn => conn.Query<Shared.Models.UserProduct>(_productPropertyQueries.GeAllFavoriteUserProductsByUserId,
                    new { @userId = user.Id })
                .ToList());

        foreach (Shared.Models.UserProduct product in userProduct)
        {
            FavoriteDto compare = new();
            Shared.Models.Product? p = await _productRep.GetAsync(product.ProductId);
            compare.userProductId = product.Id;
            compare.productId = product.ProductId;
            compare.name = p.Name;
            compare.code = p.Code;
            compare.shortLink = p.ShortLink;
            Shared.Models.ProductGallery image = DapperHelper.ExecuteCommand<Shared.Models.ProductGallery>(
                _ConnectionString,
                conn => conn
                    .Query<Shared.Models.ProductGallery>(_productQueries.GetMainImage,
                        new { @productId = product.ProductId })
                    .FirstOrDefault());
            compare.image = image.ImageName;
            compare.productStockId = await _productStockRep.GetFirstProductStockPriceIdFromProductId(product.ProductId);

            result.Add(compare);
        }

        return result;
    }
}