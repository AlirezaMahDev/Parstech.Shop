﻿using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
	public record GetProductsByCategoryByPagingQueryReq(ParameterDto Parameter, int productTypeId, int categoryId) : IRequest<ProductPageingDto>;
}
