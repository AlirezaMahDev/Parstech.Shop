using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class CreateImagesOfProductsQueryHandler : IRequestHandler<CreateImagesOfProductsQueryReq, Unit>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductGallleryRepository _productGalleryRep;
        public CreateImagesOfProductsQueryHandler(IProductRepository productRep, IProductGallleryRepository productGalleryRep)
        {
            _productRep = productRep;
            _productGalleryRep = productGalleryRep;
        }
        public async Task<Unit> Handle(CreateImagesOfProductsQueryReq request, CancellationToken cancellationToken)
        {
            var list =await _productRep.GetAll();
            foreach (var item in list) 
            { 
               Domain.Models.ProductGallery gallery=new Domain.Models.ProductGallery();
                if(await _productRep.IsChild(item.Id))
                {
                    var parrent=await _productRep.GetAsync(item.ParentId.Value);
                    gallery.IsMain = true;
                    gallery.ProductId = item.Id;
                    gallery.ImageName = $"{parrent.Code}.jpg";
                    gallery.Alt = item.Name;
                }
                else
                {
                    gallery.IsMain = true;
                    gallery.ProductId = item.Id;
                    gallery.ImageName = $"{item.Code}.jpg";
                    gallery.Alt = item.Name;
                }
                await _productGalleryRep.AddAsync(gallery);
            }
            return Unit.Value;
        }
    }
}
