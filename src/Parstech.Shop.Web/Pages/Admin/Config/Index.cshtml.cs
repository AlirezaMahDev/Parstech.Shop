using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Config;

public class IndexModel : PageModel
{
    private readonly ConfigAdminGrpcClient _configClient;
    private readonly IProductRepository _productRepository;

    public IndexModel(ConfigAdminGrpcClient configClient, IProductRepository productRepository)
    {
        _configClient = configClient;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.AddProductsByExcelAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostApi()
    {
        await _configClient.GetCreditOfNationalCodeAsync(24, "4450035887");
        return Page();
    }

    public async Task<IActionResult> OnPostImages()
    {
        // This functionality may need to be implemented in the ConfigAdminGrpcService
        return Page();
    }

    public List<resultWordpress> rw { get; set; } = new();

    public async Task<IActionResult> OnPostWordpress(int page)
    {
        List<WordpressProductDto> wordpressProducts = await _configClient.GetProductsFromWordpressAsync(page);
        // You may need to convert wordpressProducts to resultWordpress format
        return Page();
    }

    public async Task<IActionResult> OnPostVariationWordpress()
    {
        // This functionality may need to be implemented in the ConfigAdminGrpcService
        return Page();
    }

    public async Task<IActionResult> OnPostFixProductStocks()
    {
        await _configClient.FixProductStocksAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostProductList()
    {
        var list = _productRepository.DapperGetProductsByPage(0, 30);
        return Page();
    }

    public async Task<IActionResult> OnPostExcelFixProducts(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.ExcelFixProductsAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostGetProductFromWordpressById(string pid)
    {
        WordpressProductDto result = await _configClient.GetProductFromWordpressByIdAsync(pid);
        return Page();
    }

    public async Task<IActionResult> OnPostGetCateguriesFromWordpress(int pageCategury)
    {
        List<WordpressCategoryDto> result = await _configClient.GetCateguriesFromWordpressAsync(pageCategury);
        return Page();
    }

    public async Task<IActionResult> OnPostDatetimeChange()
    {
        var result = await _configClient.DatetimeChangeAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostFixDublicate()
    {
        var result = await _configClient.FixDublicateAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostEditCateguriesOfProducts(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.EditCateguriesOfProductsAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostAddUsersAndWalletCredit(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.AddUsersAndWalletCreditAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateUserWalletsCredit(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.UpdateUserWalletsCreditAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostFillProductCode(IFormFile file,
        [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        await _configClient.FillProductCodeAsync(file.FileName);
        return Page();
    }

    public async Task<IActionResult> OnPostSetBestStockId()
    {
        await _configClient.SetBestStockIdAsync();
        return Page();
    }
}