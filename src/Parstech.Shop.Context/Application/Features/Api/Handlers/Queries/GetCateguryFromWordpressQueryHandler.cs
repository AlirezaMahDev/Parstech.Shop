using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Api;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Api.Handlers.Queries;

public class GetCateguryFromWordpressQueryHandler : IRequestHandler<GetCateguryFromWordpressQueryReq, List<resultWordpress>>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;

    private readonly IUserStoreRepository _userStore;
    private readonly IPropertyRepository _propertyRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IProductGallleryRepository _productGalleryRep;

    public GetCateguryFromWordpressQueryHandler(IMediator mediator,
        IProductRepository productRep,
        ICateguryRepository categuryRep,
        IProductGallleryRepository productGalleryRep,
        IPropertyRepository propertyRep,
        IUserStoreRepository userStore, IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _productRep = productRep;
        _categuryRep = categuryRep;
        _productGalleryRep = productGalleryRep;
        _propertyRep = propertyRep;
        _userStore = userStore;
        _productStockRep = productStockRep;
    }
    public async Task<List<resultWordpress>> Handle(GetCateguryFromWordpressQueryReq request, CancellationToken cancellationToken)
    {
        //HttpClient clients = new HttpClient();
        List<resultWordpress> resultWordpresses = new();

        //var path = $"https://parstechworld.ir/wp-json/wc/v3/products/categories?&consumer_secret=cs_8207eb826d0e6b280caaa8c21ccfe35817b4e79a&consumer_key=ck_c1c3b0cb857602948565ea902b6f153b69ac0561&page={request.page}&per_page=100";
        // try
        // {
        //     HttpResponseMessage response = await clients.GetAsync(path);
        //     if (response.IsSuccessStatusCode)
        //     {
        //         var res = await response.Content.ReadAsStringAsync();
        //         var Result = JsonConvert.DeserializeObject<List<WordpressCateguryDto>>(res);

        //         foreach (var item in Result)
        //         {
        //            CateguryDto categury=new CateguryDto()
        //            {
        //                GroupTitle=item.name,
        //                IsDelete=false,
        //                ParentId=null,

        //                ChangeByUserName=null,
        //                LastChangeTime=DateTime.Now,
        //                IsParnet=false,
        //                Show=true,
        //                Image=null,
        //                BackImage=null,
        //                LatinGroupTitle = item.id.ToString(),
        //                Alt =item.parent.ToString(),
        //                Row=1

        //            };
        //             if (item.parent == 0)
        //             {
        //                 categury.IsParnet = true;
        //             }

        //             await _mediator.Send(new CateguryCreateCommandReq(categury));

        //         }


        //     }
        // }
        // catch (Exception e)
        // {
        //     resultWordpress res = new resultWordpress()
        //     {
        //         productId = 0,
        //         type = "base",
        //         error = e.InnerException.ToString()

        //     };
        //     resultWordpresses.Add(res);

        // }



        var list = await _categuryRep.GetAll();
        foreach (var item in list)
        {
            if (item.Alt != "0")
            {
                var parent = await _categuryRep.GetCateguryByLatinName(item.Alt);
                parent.IsParnet = true;

                item.ParentId = parent.GroupId;
                await _categuryRep.UpdateAsync(parent);
                await _categuryRep.UpdateAsync(item);
            }
        }

        return resultWordpresses;
    }

}