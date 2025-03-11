using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;

namespace Shop.Application.Features.ProductGallery.Requests.Queries
{
    public record CateguryOfProductQueryReq(int productId) : IRequest<ProductCateguryDto>;
}
