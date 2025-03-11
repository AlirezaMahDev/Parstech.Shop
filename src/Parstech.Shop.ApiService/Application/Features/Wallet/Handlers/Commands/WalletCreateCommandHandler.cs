using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.Wallet;
using Shop.Application.Features.Wallet.Requests.Commands;

namespace Shop.Application.Features.Wallet.Handlers.Commands
{
    public class WalletCreateCommandHandler : IRequestHandler<WalletCreateCommandReq, WalletDto>
    {
        private IWalletRepository _walletRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public WalletCreateCommandHandler(IWalletRepository walletRep, IMapper mapper, IMediator madiiator)
        {
            _walletRep = walletRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }
        public async Task<WalletDto> Handle(WalletCreateCommandReq request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Models.Wallet>(request.walletDto);
            var Result=await _walletRep.AddAsync(item);
            return _mapper.Map<WalletDto>(Result);
        }
    }
}
