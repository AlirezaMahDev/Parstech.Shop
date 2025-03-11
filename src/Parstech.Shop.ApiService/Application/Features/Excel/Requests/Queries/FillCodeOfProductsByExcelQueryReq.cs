using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Excel.Requests.Queries
{
	public record FillCodeOfProductsByExcelQueryReq(string fileName):IRequest<Unit>;

}
