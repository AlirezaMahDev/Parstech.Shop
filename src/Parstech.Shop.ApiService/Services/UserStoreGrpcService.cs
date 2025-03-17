using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class UserStoreGrpcService : UserStoreService.UserStoreServiceBase
{
    private readonly IMediator _mediator;

    public UserStoreGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<UserStoreResponse> GetStores(StoresRequest request, ServerCallContext context)
    {
        try
        {
            void stores = await _mediator.Send(new StoresQueryReq());

            var response = new UserStoreResponse();
            foreach (var store in stores)
            {
                response.Stores.Add(MapStoreToProto(store));
            }

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
            void store = await _mediator.Send(new StoreReadCommandReq(request.Id));
            return MapStoreToProto(store);
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<UserStore> GetStoreByLatinName(StoreByLatinNameRequest request,
        ServerCallContext context)
    {
        try
        {
            void store = await _mediator.Send(new UserSaleReadByLatinNameQueryReq(request.LatinName));

            return new UserStore
            {
                Id = store.Id,
                Name = store.Name ?? string.Empty,
                LatinName = store.LatinName ?? string.Empty,
                Description = store.Description ?? string.Empty,
                Image = store.Image ?? string.Empty,
                IsActive = store.IsActive,
                Address = store.Address ?? string.Empty,
                Phone = store.Phone ?? string.Empty,
                Email = store.Email ?? string.Empty,
                Website = store.Website ?? string.Empty,
                Instagram = store.Instagram ?? string.Empty,
                Telegram = store.Telegram ?? string.Empty,
                Whatsapp = store.Whatsapp ?? string.Empty,
                UserName = store.UserName ?? string.Empty,
                CreatedAt = store.CreatedAt?.ToString() ?? string.Empty,
                UpdatedAt = store.UpdatedAt?.ToString() ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    private UserStore MapStoreToProto(Shop.Application.DTOs.Store.StoreDto storeDto)
    {
        return new UserStore
        {
            Id = storeDto.Id,
            Name = storeDto.Name ?? string.Empty,
            LatinName = storeDto.LatinName ?? string.Empty,
            Description = storeDto.Description ?? string.Empty,
            Image = storeDto.Image ?? string.Empty,
            IsActive = storeDto.IsActive,
            Address = storeDto.Address ?? string.Empty,
            Phone = storeDto.Phone ?? string.Empty,
            Email = storeDto.Email ?? string.Empty,
            Website = storeDto.Website ?? string.Empty,
            Instagram = storeDto.Instagram ?? string.Empty,
            Telegram = storeDto.Telegram ?? string.Empty,
            Whatsapp = storeDto.Whatsapp ?? string.Empty,
            UserName = storeDto.UserName ?? string.Empty,
            CreatedAt = storeDto.CreatedAt?.ToString() ?? string.Empty,
            UpdatedAt = storeDto.UpdatedAt?.ToString() ?? string.Empty
        };
    }
}