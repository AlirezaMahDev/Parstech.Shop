using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Excel.Requests.Queries
{
    public record EditCateguriesOfProductQueryReq(string fileName):IRequest<Unit>;

}
