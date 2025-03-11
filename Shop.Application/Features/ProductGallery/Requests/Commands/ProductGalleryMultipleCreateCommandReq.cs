using MediatR;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductGallery.Requests.Commands
{
    public record ProductGalleryMultipleCreateCommandReq(UploadViewModel items,int productId) :IRequest<ResponseDto>;

}
