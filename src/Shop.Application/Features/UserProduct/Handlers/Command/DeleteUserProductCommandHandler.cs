using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.UserProduct.Requests.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserProduct.Handlers.Command
{
    public class DeleteUserProductCommandHandler : IRequestHandler<DeleteUserProductCommandReq>
    {
        private readonly IUserProductRepository _userProductRep;
        private readonly IMapper _mapper;

        public DeleteUserProductCommandHandler(IUserProductRepository userProductRep, IMapper mapper)
        {
            _userProductRep = userProductRep;
            _mapper = mapper;
        }
        public async Task Handle(DeleteUserProductCommandReq request, CancellationToken cancellationToken)
        {
            var userProduct = await _userProductRep.GetAsync(request.userProductId);
            await _userProductRep.DeleteAsync(userProduct);
        }
    }
}
