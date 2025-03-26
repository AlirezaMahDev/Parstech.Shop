using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.UserProduct.Requests.Command;

namespace Parstech.Shop.Context.Application.Features.UserProduct.Handlers.Command;

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