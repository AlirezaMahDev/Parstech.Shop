using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.ApiService.Services;

public class UserStoreService : UserStoreServiceBase
{
    private readonly IMediator _mediator;
    private readonly IUserStoreRepository _userStoreRepository;

    public UserStoreService(IMediator mediator, IUserStoreRepository userStoreRepository)
    {
        _mediator = mediator;
        _userStoreRepository = userStoreRepository;
    }

    public override async Task<UserStoreResponse> GetStores(StoresRequest request, ServerCallContext context)
    {
        try
        {
            var command = new UserStoreReadsCommandReq();
            void stores = await _mediator.Send(command);

            var response = new UserStoreResponse();
            response.Stores.AddRange(stores.Select(s => new UserStore
            {
                Id = s.Id,
                Name = s.Name,
                LatinName = s.LatinName,
                Description = s.Description,
                Image = s.Image,
                IsActive = s.IsActive,
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                Website = s.Website,
                Instagram = s.Instagram,
                Telegram = s.Telegram,
                Whatsapp = s.Whatsapp,
                UserName = s.UserName,
                CreatedAt = s.CreatedAt.ToString(),
                UpdatedAt = s.UpdatedAt.ToString()
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<UserStore> GetStoreById(StoreByIdRequest request, ServerCallContext context)
    {
        try
        {
            var store = await _userStoreRepository.GetById(request.Id);
            if (store == null)
            {
                throw new RpcException(new(StatusCode.NotFound, "Store not found"));
            }

            return new UserStore
            {
                Id = store.Id,
                Name = store.Name,
                LatinName = store.LatinName,
                Description = store.Description,
                Image = store.Image,
                IsActive = store.IsActive,
                Address = store.Address,
                Phone = store.Phone,
                Email = store.Email,
                Website = store.Website,
                Instagram = store.Instagram,
                Telegram = store.Telegram,
                Whatsapp = store.Whatsapp,
                UserName = store.UserName,
                CreatedAt = store.CreatedAt.ToString(),
                UpdatedAt = store.UpdatedAt.ToString()
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}