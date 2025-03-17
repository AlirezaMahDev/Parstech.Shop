using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Command;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Handlers.Command;

public class CreateUserProductCommandHandler : IRequestHandler<CreateUserProductCommandReq, bool>
{
    private readonly IUserProductRepository _userProductRep;
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;

    public CreateUserProductCommandHandler(IUserProductRepository userProductRep,
        IMapper mapper,
        IUserRepository userRep)
    {
        _userProductRep = userProductRep;
        _mapper = mapper;
        _userRep = userRep;
    }

    public async Task<bool> Handle(CreateUserProductCommandReq request, CancellationToken cancellationToken)
    {
        bool exist = false;
        if (request.type == "Compare")
        {
            exist = await _userProductRep.ExistFourUserProductByUserName(request.userName, "Compare");
        }

        Shared.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        if (!exist)
        {
            UserProductDto userProductDto = new()
            {
                ProductId = request.productId, UserId = user.Id, Type = request.type
            };
            Shared.Models.UserProduct? up = _mapper.Map<Shared.Models.UserProduct>(userProductDto);
            await _userProductRep.AddAsync(up);
            return true;
        }
        else
        {
            return false;
        }
    }
}