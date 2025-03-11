using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;

namespace Shop.Application.Features.ProductCategury.Requests.Commands
{
    public record ProductCateguryUpdateCommandReq(ProductCateguryDto ProductCateguryDto) : IRequest<ProductCateguryDto>;

}
