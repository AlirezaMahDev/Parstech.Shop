using AutoMapper;
using Azure;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductGallery.Handlers.Commands
{
    public class ChangeMainGalleryCommandHandler : IRequestHandler<ChangeMainGalleryCommandReq, ResponseDto>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;

        public ChangeMainGalleryCommandHandler(IProductGallleryRepository productGallleryRep,
            IProductRepository productRep, IMapper mapper)
        {
            _productGallleryRep = productGallleryRep;
            _productRep = productRep;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Handle(ChangeMainGalleryCommandReq request, CancellationToken cancellationToken)
        {
            var galleries = await _productGallleryRep.GetGalleriesByProduct(request.productId);
            foreach (var gallery in galleries)
            {
                if (gallery.Id != request.galleryId)
                {
                    if (gallery.IsMain)
                    {
                        gallery.IsMain = false;
                        await _productGallleryRep.UpdateAsync(gallery);
                    }

                }
                else
                {
                    if (!gallery.IsMain)
                    {
                        gallery.IsMain = true;
                        await _productGallleryRep.UpdateAsync(gallery);
                    }
                }
            }
            ResponseDto Response=new ResponseDto();
            Response.IsSuccessed = true;
            Response.Message = "عملیات با موفقیت انجام شد";
            return Response;
        }
    }
}
