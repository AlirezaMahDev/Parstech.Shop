﻿using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.Dapper.ProductProperty.Queries;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.Features.UserProduct.Requests.Query;

namespace Parstech.Shop.Context.Application.Features.UserProduct.Handlers.Query;

public class GetCmparisonProductsOfUsersQueryHandler : IRequestHandler<GetCmparisonProductsOfUsersQueryReq, List<CompareDto>>
{
    private readonly IProductPropertyQueries _productPropertyQueries;
    private readonly IProductQueries _productQueries;
    private readonly IConfiguration configuration;
    private readonly IUserRepository _userRep;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly String _ConnectionString;
    public GetCmparisonProductsOfUsersQueryHandler(IProductPropertyQueries productPropertyQueries,
        IConfiguration configuration,
        IProductQueries productQueries,
        IUserRepository userRep,
        IProductStockPriceRepository productStockPriceRep,
        IProductRepository productRep)
    {
        _productPropertyQueries = productPropertyQueries;
        _userRep = userRep;
        _productQueries = productQueries;
        _ConnectionString = configuration.GetConnectionString("DatabaseConnection");
        _productStockPriceRep = productStockPriceRep;
        _productRep = productRep;
    }

    public class TwoProp
    {
        public int PropertyId { get; set; }
        public string Caption { get; set; }
    }
    public class ProductsTwoProps
    {
        public List<TwoProp> TwoProps { get; set; }
    }

    public List<ProductsTwoProps> AllproductsTwoProps { get; set; } = new();
    public async Task<List<CompareDto>> Handle(GetCmparisonProductsOfUsersQueryReq request, CancellationToken cancellationToken)
    {
        List<CompareDto> result = new();
        var user = await _userRep.GetUserByUserName(request.userName);
        var userProduct = DapperHelper.ExecuteCommand<List<Domain.Models.UserProduct>>(_ConnectionString, conn => conn.Query<Domain.Models.UserProduct>(_productPropertyQueries.GeAllCompareUserProductsByUserId, new { @userId = user.Id }).ToList());
        List<List<object>> allLists = new();

        if (userProduct.Count > 0)
        {
            foreach (var product in userProduct)
            {
                List<ProductPropertyDto> list = DapperHelper.ExecuteCommand<List<ProductPropertyDto>>(_ConnectionString, conn => conn.Query<ProductPropertyDto>(_productPropertyQueries.GeAllByProductId, new { @productId = product.ProductId }).ToList());
                List<object> twoList = new();

                foreach (var Item in list)
                {
                    object ob = new { Name = Item.Caption };
                    twoList.Add(ob);
                }

                allLists.Add(twoList);
            }
            var commonItems = allLists.First();
            foreach (var list in allLists.Skip(1))
            {
                commonItems = commonItems.Intersect(list, new ObjectComparer()).ToList();
            }



            foreach (var product in userProduct)
            {
                CompareDto compare = new();
                List<ProductPropertyDto> common = new();
                List<ProductPropertyDto> all = new();

                List<ProductPropertyDto> list = DapperHelper.ExecuteCommand<List<ProductPropertyDto>>(_ConnectionString, conn => conn.Query<ProductPropertyDto>(_productPropertyQueries.GeAllByProductId, new { @productId = product.ProductId }).ToList());
                foreach (var item in commonItems)
                {

                    var name = item.GetType().GetProperty("Name").GetValue(item, null);

                    if (list.Any(u => u.Caption == name.ToString()))
                    {
                        var com = list.FirstOrDefault(u => u.Caption == name.ToString());
                        common.Add(com);
                        list.Remove(com);
                    }
                    if (list.Count > 0)
                    {
                        var one = list.First();
                        compare.code = one.Code;
                        compare.name = one.Name;
                    }


                }
                all = list;
                compare.userProductId = product.Id;
                compare.productId = product.ProductId;


                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_ConnectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.ProductId }).FirstOrDefault());
                compare.image = image.ImageName;

                compare.commonProperties = common;
                var p = await _productRep.GetAsync(product.ProductId);
                compare.shortLink = p.ShortLink;
                compare.productProperties = all;
                compare.productStockId= compare.productStockId = await _productStockPriceRep.GetFirstProductStockPriceIdFromProductId(product.ProductId);
                result.Add(compare);
            }
        }
            


        return result;
    }

    public class ObjectComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            var xName = (string)x.GetType().GetProperty("Name").GetValue(x, null);
            var yName = (string)y.GetType().GetProperty("Name").GetValue(y, null);
            return xName == yName;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetType().GetProperty("Name").GetValue(obj, null).GetHashCode();
        }
    }





}