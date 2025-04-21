using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.Dapper.ProductProperty.Queries;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.UserProduct;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.UserProduct.Requests.Query;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shop.Application.Features.UserProduct.Handlers.Query.GetCmparisonProductsOfUsersQueryHandler;

namespace Shop.Application.Features.UserProduct.Handlers.Query
{
    public class GetFavoriteProductOfUsersQueryHandler : IRequestHandler<GetFavoriteProductOfUsersQueryReq, List<FavoriteDto>>
    {
        private readonly IProductPropertyQueries _productPropertyQueries;
        private readonly IProductQueries _productQueries;
        private readonly IConfiguration configuration;
        private readonly IUserRepository _userRep;
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly String _ConnectionString;
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
        public async Task<List<FavoriteDto>> Handle(GetFavoriteProductOfUsersQueryReq request, CancellationToken cancellationToken)
        {

            List<FavoriteDto> result = new List<FavoriteDto>();
            var user = await _userRep.GetUserByUserName(request.userName);
            var userProduct = DapperHelper.ExecuteCommand<List<Domain.Models.UserProduct>>(_ConnectionString, conn => conn.Query<Domain.Models.UserProduct>(_productPropertyQueries.GeAllFavoriteUserProductsByUserId, new { @userId = user.Id }).ToList());
            
            foreach (var product in userProduct)
            {
                FavoriteDto compare = new FavoriteDto();
                var p= await _productRep.GetAsync(product.ProductId);
                compare.userProductId = product.Id;
                compare.productId = product.ProductId;
                compare.name = p.Name;
                compare.code = p.Code;
                compare.shortLink = p.ShortLink;
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_ConnectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.ProductId }).FirstOrDefault());
                compare.image = image.ImageName;
                compare.productStockId =await _productStockRep.GetFirstProductStockPriceIdFromProductId(product.ProductId);
                
                result.Add(compare);
            }

            return result;
        }
    }
}
