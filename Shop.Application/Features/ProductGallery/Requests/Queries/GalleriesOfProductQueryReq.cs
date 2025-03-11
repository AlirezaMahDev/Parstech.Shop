using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;

namespace Shop.Application.Features.ProductGallery.Requests.Queries
{
    public record GalleriesOfProductQueryReq(int productId) : IRequest<List<ProductGalleryDto>>;
}
