using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductGallery.Requests.Commands
{
    public record ChangeMainGalleryCommandReq(int galleryId,int productId):IRequest<ResponseDto>;

}
