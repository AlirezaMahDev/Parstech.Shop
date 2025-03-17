using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class TorobService : TorobServiceBase
{
    private readonly IMediator _mediator;

    public TorobService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<Torob> GetTorobProduct(TorobRequest request, ServerCallContext context)
    {
        try
        {
            void torob = await _mediator.Send(new TorobGetProductQueryReq(request.StoreId, request.BaseUrl));

            return new Torob
            {
                ProductId = torob.ProductId,
                PageUrl = torob.PageUrl,
                Price = torob.Price,
                Availability = torob.Availability,
                OldPrice = torob.OldPrice,
                Image = torob.Image,
                Content = torob.Content,
                Name = torob.Name
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}