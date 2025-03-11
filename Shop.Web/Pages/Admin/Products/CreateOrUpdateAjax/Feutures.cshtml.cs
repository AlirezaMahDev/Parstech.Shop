using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.PropertyCategury.Requests.Commands;
using Shop.Domain.Models;

namespace Shop.Web.Pages.Admin.Products.Detail
{
    public class FeuturesModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        
        private readonly IProductStockPriceRepository _productStockRep;


        public FeuturesModel(IMediator mediator,
           
            IProductStockPriceRepository productStockRep)
        {
            _mediator = mediator;
           
            _productStockRep = productStockRep;
        }

        #endregion
        //id
        [BindProperty] public int? productId { get; set; }

        [BindProperty]
        public List<ProductPropertyDto> PropertyDtos { get; set; }

        public async Task OnGet(int? id)
        {
            productId = id;
            if (productId != null)
            {
                PropertyDtos = await _mediator.Send(new PropertiesOfProductQueryReq(productId.Value));
            }
        }
    }
}
