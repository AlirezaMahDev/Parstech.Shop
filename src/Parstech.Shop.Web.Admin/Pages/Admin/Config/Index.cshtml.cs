using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Api;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Config;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepository;
        
    public IndexModel(IMediator mediator, IProductRepository productRepository)
    {
        _mediator = mediator;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> OnGet()
    {

        return Page();
    }

    public async Task<IActionResult> OnPost(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new AddProductsByExcelQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostApi()
    {
        await _mediator.Send(new GetCreditOfNationalCodeQueryReq(24, "4450035887"));
        return Page();
    }

    public async Task<IActionResult> OnPostImages()
    {
        await _mediator.Send(new CreateImagesOfProductsQueryReq());
        return Page();
    }

    public List<resultWordpress> rw { get; set; } = new();
    public async Task<IActionResult> OnPostWordpress(int page)
    {


        rw = await _mediator.Send(new GetProductsFromWordpressQueryReq(page));
        return Page();
    }
    public async Task<IActionResult> OnPostVariationWordpress()
    {


        await _mediator.Send(new GetvariationsFromWordpressQueryReq());
        return Page();
    }
    public async Task<IActionResult> OnPostFixProductStocks()
    {


        await _mediator.Send(new FixproductStockPriceQueryReq());
        return Page();
    }
    public async Task<IActionResult> OnPostProductList()
    {
        var list = _productRepository.DapperGetProductsByPage(0, 30);
        return Page();
    }

    public async Task<IActionResult> OnPostExcelFixProducts(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new FixRezerveProductQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostGetProductFromWordpressById(string pid)
    {
        var result = await _mediator.Send(new GetProductFromWordpressQueryReq(pid));
        return Page();
    }
    public async Task<IActionResult> OnPostGetCateguriesFromWordpress(int pageCategury)
    {
        var result = await _mediator.Send(new GetCateguryFromWordpressQueryReq(pageCategury));
        return Page();
    }

    public async Task<IActionResult> OnPostDatetimeChange()
    {
        var result = await _mediator.Send(new ChangeDatetimeProductsQueryReq());
        return Page();
    }
    public async Task<IActionResult> OnPostFixDublicate()
    {
        var result = await _mediator.Send(new FixDuplicateProductsByCodeQueryReq());
        return Page();
    }

    public async Task<IActionResult> OnPostEditCateguriesOfProducts(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new EditCateguriesOfProductQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostAddUsersAndWalletCredit(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new AddUsersAndWalletsByExcelQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateUserWalletsCredit(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new UpdateUserWalletsByExcelQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostFillProductCode(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        string filename = $"{hostingEnvironment.WebRootPath}\\Shared\\Files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(filename))
        {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }
        //var ProductExcelDto = this.GetBudgetList(file.FileName);

        await _mediator.Send(new FillCodeOfProductsByExcelQueryReq(file.FileName));
        return Page();
    }

    public async Task<IActionResult> OnPostSetBestStockId()
    {
        await _productRepository.RefreshAllBestStockProduct();
        return Page();
    }
}