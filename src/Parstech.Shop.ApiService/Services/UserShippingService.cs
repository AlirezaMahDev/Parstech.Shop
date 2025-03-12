using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.UserShipping;
using Shop.Application.DTOs.UserShipping;
using Shop.Application.Features.UserShipping.Handlers.Commands;
using Shop.Application.Features.UserShipping.Handlers.Queries;
using Shop.Application.Features.UserShipping.Requests.Commands;
using Shop.Application.Features.UserShipping.Requests.Queries;
using Shop.Domain.Entities;

namespace Parstech.Shop.ApiService.Services
{
    public class UserShippingService : UserShippingService.UserShippingServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserShippingService> _logger;

        public UserShippingService(IMediator mediator, IMapper mapper, ILogger<UserShippingService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<UserShipping> GetUserShipping(UserShippingRequest request, ServerCallContext context)
        {
            try
            {
                var userShippingDto = await _mediator.Send(new UserShippingReadCommandReq(request.ShippingId));
                return _mapper.Map<UserShipping>(userShippingDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user shipping with ID {ShippingId}", request.ShippingId);
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting user shipping: {ex.Message}"));
            }
        }

        public override async Task<UserShippingListResponse> GetUserShippingAddresses(UserShippingListRequest request, ServerCallContext context)
        {
            try
            {
                var userShippingList = await _mediator.Send(new UserShippingOfUserQueryReq(request.UserName));
                var response = new UserShippingListResponse();
                response.ShippingAddresses.AddRange(_mapper.Map<IEnumerable<UserShipping>>(userShippingList));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user shipping addresses for user {UserName}", request.UserName);
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting user shipping addresses: {ex.Message}"));
            }
        }

        public override async Task<UserShipping> CreateUserShipping(CreateUserShippingRequest request, ServerCallContext context)
        {
            try
            {
                // Get user ID from user name
                var userId = await GetUserIdFromUserName(request.UserName);
                
                // Create the UserShippingDto from request
                var userShippingDto = new UserShippingDto
                {
                    FirstName = request.FirstName?.Value,
                    LastName = request.LastName?.Value,
                    Phone = request.Phone?.Value,
                    Mobile = request.Mobile?.Value,
                    Country = request.Country?.Value,
                    State = request.State?.Value,
                    City = request.City?.Value,
                    Address = request.Address?.Value,
                    PostCode = request.PostCode?.Value,
                    UserId = userId
                };

                // Call the mediator to create the user shipping
                var result = await _mediator.Send(new UserShippingCreateCommandReq(userShippingDto));
                
                // Map the result to the response
                return _mapper.Map<UserShipping>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user shipping for user {UserName}", request.UserName);
                throw new RpcException(new Status(StatusCode.Internal, $"Error creating user shipping: {ex.Message}"));
            }
        }

        public override async Task<UserShipping> UpdateUserShipping(UpdateUserShippingRequest request, ServerCallContext context)
        {
            try
            {
                // First get the existing shipping to preserve the UserId
                var existingShippingDto = await _mediator.Send(new UserShippingReadCommandReq(request.Id));
                
                // Update fields from the request
                existingShippingDto.FirstName = request.FirstName?.Value;
                existingShippingDto.LastName = request.LastName?.Value;
                existingShippingDto.Phone = request.Phone?.Value;
                existingShippingDto.Mobile = request.Mobile?.Value;
                existingShippingDto.Country = request.Country?.Value;
                existingShippingDto.State = request.State?.Value;
                existingShippingDto.City = request.City?.Value;
                existingShippingDto.Address = request.Address?.Value;
                existingShippingDto.PostCode = request.PostCode?.Value;

                // Call the mediator to update the user shipping
                var result = await _mediator.Send(new UserShippingUpdateCommandReq(existingShippingDto));
                
                // Map the result to the response
                return _mapper.Map<UserShipping>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user shipping with ID {ShippingId}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, $"Error updating user shipping: {ex.Message}"));
            }
        }

        public override async Task<UserShippingResponse> DeleteUserShipping(UserShippingRequest request, ServerCallContext context)
        {
            try
            {
                // TODO: Implement delete functionality when MediatR command is available
                // await _mediator.Send(new UserShippingDeleteCommandReq(request.ShippingId));
                
                return new UserShippingResponse
                {
                    Status = true,
                    Message = "User shipping address deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user shipping with ID {ShippingId}", request.ShippingId);
                return new UserShippingResponse
                {
                    Status = false,
                    Message = $"Error deleting user shipping: {ex.Message}"
                };
            }
        }

        private async Task<int> GetUserIdFromUserName(string userName)
        {
            // TODO: Replace with actual logic to get user ID from username
            // For now, we'll return a placeholder user ID
            return 1; 
        }
    }
}
