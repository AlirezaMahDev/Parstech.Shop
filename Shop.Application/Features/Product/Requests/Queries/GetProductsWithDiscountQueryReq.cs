using MediatR;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
	public record GetProductsWithDiscountQueryReq(int take,int sectionId) : IRequest<List<ProductDto>>;
}
