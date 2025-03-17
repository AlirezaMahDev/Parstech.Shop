using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parstech.Shop.Web.Controllers;

[Route("TorobRequest")]
[ApiController]
public class TorobController : ControllerBase
{
    private readonly TorobGrpcClient _torobClient;

    public TorobController(TorobGrpcClient torobClient)
    {
        _torobClient = torobClient;
    }

    // GET: api/<TorobController>
    [HttpGet("{page}")]
    public async Task<IActionResult> Get(int page)
    {
        List<TorobDto> Response = new();
        var result = await _torobClient.GetTorobProductsAsync(page);
        string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";

        foreach (var item in result)
        {
            string price = "";
            string Oldprice = "";
            string availability = "";

            if (item.Quantity > 0 && item.SalePrice > 0)
            {
                availability = "instock";
            }
            else
            {
                availability = "outofstock";
            }

            if (item.DiscountPrice > 0)
            {
                price = item.DiscountPrice.ToString();
                Oldprice = item.SalePrice.ToString();
            }
            else
            {
                price = item.SalePrice.ToString();
                Oldprice = item.SalePrice.ToString();
            }

            TorobDto torobItem = new TorobDto()
            {
                product_id = item.Id.ToString(),
                page_url = $"{baseUrl}/Products/Detail/{item.ShortLink}/{item.Id}",
                price = price,
                old_price = Oldprice,
                availability = availability
            };

            Response.Add(torobItem);
        }

        return Ok(Response);
    }
}