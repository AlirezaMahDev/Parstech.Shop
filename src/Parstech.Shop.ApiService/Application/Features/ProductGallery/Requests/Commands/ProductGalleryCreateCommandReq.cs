using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.Response;

namespace Shop.Application.Features.ProductGallery.Requests.Commands
{
    public record ProductGalleryCreateCommandReq(ProductGalleryDto ProductGalleryDto) : IRequest<ResponseDto>;

}
