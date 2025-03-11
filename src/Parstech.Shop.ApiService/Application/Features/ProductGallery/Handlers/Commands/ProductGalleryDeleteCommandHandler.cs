using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Generator;
using Shop.Domain.Models;

namespace Shop.Application.Features.ProductGallery.Handlers.Commands
{
    public class ProductGalleryDeleteCommandHandler : IRequestHandler<ProductGalleryDeleteCommandReq, Unit>
    {
        private readonly IProductGallleryRepository _productGalleryRep;
        private readonly IMapper _mapper;

        public ProductGalleryDeleteCommandHandler(IProductGallleryRepository productGalleryRep, IMapper mapper)
        {
            _productGalleryRep = productGalleryRep;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ProductGalleryDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var pgallery = await _productGalleryRep.GetAsync(request.id);

            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", pgallery.ImageName);
            using (FileStream fs = new FileStream(tempFile, FileMode.Open)) { }
            try
            {
                File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }

            await _productGalleryRep.DeleteAsync(pgallery);
            return Unit.Value;
        }
    }
}
