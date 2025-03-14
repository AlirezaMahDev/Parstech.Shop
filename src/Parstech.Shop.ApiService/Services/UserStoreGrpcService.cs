using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.UserStore;
using Shop.Application.Features.Store.Requests.Queries;

namespace Shop.ApiService.Services
{
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
                var stores = await _mediator.Send(new StoresQueryReq());
                
                var response = new UserStoreResponse();
                foreach (var store in stores)
                {
                    response.Stores.Add(MapStoreToProto(store));
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<UserStore> GetStoreById(StoreByIdRequest request, ServerCallContext context)
        {
            try
            {
                var store = await _mediator.Send(new StoreReadCommandReq(request.Id));
                return MapStoreToProto(store);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
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
} 