using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Shared.Protos.ProductComponentsAdmin;
using Parstech.Shop.Web.GrpcClients;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.PropertyCategury.Requests.Commands;
using Shop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Products.Detail
{
    public class FeuturesModel : PageModel
    {
        #region Constructor

        private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
        private readonly IProductStockPriceRepository _productStockRep;

        public FeuturesModel(
            IProductComponentsAdminGrpcClient productComponentsClient,
            IProductStockPriceRepository productStockRep)
        {
            _productComponentsClient = productComponentsClient;
            _productStockRep = productStockRep;
        }

        #endregion
        
        //id
        [BindProperty] public int? productId { get; set; }

        [BindProperty]
        public List<ProductPropertyDto> PropertyDtos { get; set; } = new List<ProductPropertyDto>();

        public async Task OnGet(int? id)
        {
            productId = id;
            if (productId != null)
            {
                var response = await _productComponentsClient.GetPropertiesOfProductAsync(productId.Value);
                if (response.IsSuccess)
                {
                    PropertyDtos = response.Properties.ToList();
                }
            }
        }
    }
}
