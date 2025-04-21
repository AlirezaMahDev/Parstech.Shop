using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Application.DTOs.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.State.Requests.Commands
{
    public record StatesReadsCommandReq():IRequest<List<SteteDto>>;

}
