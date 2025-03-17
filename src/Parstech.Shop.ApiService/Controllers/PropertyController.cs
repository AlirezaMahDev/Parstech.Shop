using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.ApiService.Application.Dapper.ProductProperty.Queries;

namespace Parstech.Shop.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly GetProductPropertiesByParrentIdQueryHandler _getProductPropertiesHandler;

    public PropertyController(IConfiguration configuration)
    {
        _getProductPropertiesHandler = new GetProductPropertiesByParrentIdQueryHandler(configuration);
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetPropertiesByProductId(long productId)
    {
        try
        {
            var properties = await _getProductPropertiesHandler.ExecuteAsync(productId);
            return Ok(properties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 