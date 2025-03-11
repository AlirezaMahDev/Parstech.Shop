using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.ProductGallery.Requests.Commands
{
    public record ProductGalleryDeleteCommandReq(int id) : IRequest<Unit>;
    
}
