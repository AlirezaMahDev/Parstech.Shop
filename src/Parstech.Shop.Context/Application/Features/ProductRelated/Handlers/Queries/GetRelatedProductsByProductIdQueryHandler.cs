﻿using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Enum;
using Parstech.Shop.Context.Application.Features.ProductRelated.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductRelated.Handlers.Queries;

public class GetRelatedProductsByProductIdQueryHandler : IRequestHandler<GetRelatedProductsByProductIdQueryReq, List<ProductDto>>
{
    private readonly IProductRelatedRepository _productRelatedRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _GalleryRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;
    private readonly IProductQueries _productQueries;
    private readonly string _connectionString;
    public GetRelatedProductsByProductIdQueryHandler(
        IProductRelatedRepository productRelatedRep,
        IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IProductGallleryRepository GalleryRep,
        IMapper mapper,
        IProductQueries productQueries,
        IConfiguration configuration,
        IProductStockPriceRepository productStockPriceRep,
        IUserRepository userRep)
    {
        _productRelatedRep = productRelatedRep;
        _productRep = productRep;
        _GalleryRep = GalleryRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _productStockPriceRep = productStockPriceRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _userRep = userRep;
    }
    public async Task<List<ProductDto>> Handle(GetRelatedProductsByProductIdQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductDto> Result = new();
        var product =await _productRep.GetAsync(request.productId);
        var sql = $"SELECT dbo.Product.Name,dbo.Product.Id, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId,dbo.UserStore.StoreName,dbo.ProductStockPrice.CateguryOfUserId,dbo.ProductStockPrice.CateguryOfUserType FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id where dbo.Product.TypeId!=2 and( dbo.Product.ParentId={product.Id}  or dbo.ProductStockPrice.ProductId={product.Id}) ORDER BY dbo.ProductStockPrice.SalePrice asc";

        var list = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(sql).ToList());

        foreach (var item in list)
        {
            var valid = true;
            #region Check UserCategury
            var existUserCategury = false;
            if (request.userName != null)
            {
                existUserCategury = await _userRep.ExistUserCategury(request.userName);  
            }
            if (item.CateguryOfUserId != null)
            {
                if (!existUserCategury)
                {
                    if (item.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                    {
                        item.DiscountPrice = 0;
                    }
                    else if (item.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                    {
                        valid = false;
                    }
                }
                   
            }
            #endregion
            if (valid)
            {
                var dto = _mapper.Map<ProductDto>(item);
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.Id }).FirstOrDefault());
                if (image != null)
                {
                    dto.Image = image.ImageName;
                }

                dto.ProductStockPriceId = item.Id;
                //if (item.TypeId == 2)
                //{

                //    var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation, new { @parentId = 6633 }).ToList());
                //    if (variations.Count > 0)
                //    {
                //        var variation = variations.FirstOrDefault();
                //        dto.ProductStockPriceId = variation.Id;
                //        dto.Quantity = variation.Quantity;
                //        dto.SalePrice = variation.SalePrice;
                //        dto.DiscountPrice = variation.DiscountPrice;
                //    }
                //    else
                //    {
                //        dto.ProductStockPriceId = item.Id;
                //    }
                //}
                //else
                //{
                //    dto.ProductStockPriceId = item.Id;
                //}

                Result.Add(dto);
            }
                
        }

        return Result;
    }
}