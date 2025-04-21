using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserProduct;
using Shop.Application.Features.UserProduct.Requests.Command;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserProduct.Handlers.Command
{
    public class CreateUserProductCommandHandler : IRequestHandler<CreateUserProductCommandReq,bool>
    {
        private readonly IUserProductRepository _userProductRep;
        private readonly IUserRepository _userRep;
        private readonly IMapper _mapper;

        public CreateUserProductCommandHandler(IUserProductRepository userProductRep,
            IMapper mapper,IUserRepository userRep)
        {
            _userProductRep = userProductRep;
            _mapper = mapper;
            _userRep = userRep;
        }
        public async Task<bool> Handle(CreateUserProductCommandReq request, CancellationToken cancellationToken)
        {
            var exist = false;
            if (request.type == "Compare")
            {
                exist = await _userProductRep.ExistFourUserProductByUserName(request.userName, "Compare");
            }
            
            var user =await _userRep.GetUserByUserName(request.userName);
            if (!exist)
            {
                UserProductDto userProductDto=new UserProductDto()
                {
                    ProductId=request.productId,
                    UserId=user.Id,
                    Type=request.type
                   
                };
                var up=_mapper.Map<Domain.Models.UserProduct>(userProductDto);
                await _userProductRep.AddAsync(up);
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
