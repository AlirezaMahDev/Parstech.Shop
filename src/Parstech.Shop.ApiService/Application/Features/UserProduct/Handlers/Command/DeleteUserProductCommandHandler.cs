using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Command;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Handlers.Command;

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
        Domain.Models.UserProduct? userProduct = await _userProductRep.GetAsync(request.userProductId);
        await _userProductRep.DeleteAsync(userProduct);
    }
}