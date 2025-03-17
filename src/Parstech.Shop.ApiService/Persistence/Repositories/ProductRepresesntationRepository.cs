using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductRepresesntationRepository : GenericRepository<ProductRepresentation>,
    IProductRepresesntationRepository
{
    private readonly DatabaseContext _context;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductRepresesntationRepository(DatabaseContext context,
        IProductStockPriceRepository productStockRep,
        IProductRepository productRep,
        IUserBillingRepository userBillingRep,
        IMediator mediator,
        IMapper mapper) : base(context)
    {
        _context = context;
        _productRep = productRep;
        _userBillingRep = userBillingRep;
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }


    public async Task<ProductRepresentation> GetProductRepresentationOfProduct(int productId)
    {
        ProductRepresentation result = new();
        if (_context.ProductRepresentations.Any(u => u.ProductStockPriceId == productId))
        {
            result = await _context.ProductRepresentations.FirstOrDefaultAsync(u => u.ProductStockPriceId == productId);
        }

        return result;
    }

    public async Task<List<ProductRepresentation>> GetUniqProductRepresentationFromRepId(int repId)
    {
        return await _context.ProductRepresentations.Where(u => u.RepresntationId == repId).ToListAsync();
    }

    public async Task<int> GetLastQuantityFromProductRepresntation(int productId, int repId)
    {
        int quantity = 0;
        List<ProductRepresentation> list = await _context.ProductRepresentations
            .Where(u => u.ProductStockPriceId == productId && u.RepresntationId == repId)
            .ToListAsync();
        foreach (ProductRepresentation? item in list)
        {
            switch (item.TypeId)
            {
                //ورود به انبار
                case 1:
                    quantity += item.Quantity;
                    break;

                //خروج از انبار (فروش)
                case 2:
                    quantity -= item.Quantity;
                    break;

                //برگشت از فروش (عودت)
                case 3:
                    quantity += item.Quantity;
                    break;

                //ورود به انبار به صورت دستی (رفع مغایرت)
                case 4:
                    quantity += item.Quantity;
                    break;
                default:
                    break;
            }
        }

        return quantity;
    }
    ////ask this
    //public async Task<ProductRepresentationPagingDto> GetAllProductRepresenattionByPaging(int Pageid = 1, int Take = 30, string Filter = "", int Rep = 0)
    //{
    //    var dtos = await _context.Products.Where(u => u.RepId == Rep).ToListAsync();
    //    ProductRepresentationPagingDto response = new ProductRepresentationPagingDto();

    //    if (!string.IsNullOrEmpty(Filter))
    //    {
    //        dtos = dtos.Where(p =>
    //            (p.Name.Contains(Filter) || p.Code.Contains(Filter))).ToList();
    //    }

    //    int skip = (Pageid - 1) * Take;

    //    response.CurrentPage = Pageid;
    //    int count = dtos.Count();
    //    response.PageCount = count / Take;
    //    response.List = dtos.Skip(skip).Take(Take).ToArray();

    //    return response;
    //}

    public async Task<List<ProductRepresentation>> GetProductRepresentationsWithRepAndProductId(int repId,
        int productId)
    {
        return await _context.ProductRepresentations
            .Where(u => u.RepresntationId == repId && u.ProductStockPriceId == productId)
            .ToListAsync();
    }

    public async Task UpdateProductQuantityByProductRepresentationId(int productRepresentationId)
    {
        ProductRepresentation? Pr = await GetAsync(productRepresentationId);
        ProductStockPrice? product = await _productStockRep.GetAsync(Pr.ProductStockPriceId);

        if (Pr.TypeId == 2)
        {
            product.Quantity -= Pr.Quantity;
        }
        else
        {
            product.Quantity += Pr.Quantity;
        }

        await _productStockRep.UpdateAsync(product);
    }

    public async Task<List<ProductRepresentation>> GetEnterProductRepresentationWithProductId(int productId)
    {
        return await _context.ProductRepresentations.Where(u => u.ProductStockPriceId == productId && u.TypeId == 1)
            .ToListAsync();
    }

    public async Task<List<ProductRepresentation>> GetGetoutProductRepresentationWithProductId(int productId)
    {
        return await _context.ProductRepresentations.Where(u => u.ProductStockPriceId == productId && u.TypeId == 2)
            .ToListAsync();
    }

    public async Task<List<ProductRepresentation>> GetReturnProductRepresentationWithProductId(int productId)
    {
        return await _context.ProductRepresentations.Where(u => u.ProductStockPriceId == productId && u.TypeId == 3)
            .ToListAsync();
    }

    public async Task<List<ProductRepresentation>> GetEnterManualyProductRepresentationWithProductId(int productId)
    {
        return await _context.ProductRepresentations.Where(u => u.ProductStockPriceId == productId && u.TypeId == 4)
            .ToListAsync();
    }

    public async Task<int> GetCountEnterProductRepresentationWithProductId(int productId)
    {
        int result = 0;
        List<ProductRepresentation> list = await GetEnterProductRepresentationWithProductId(productId);
        foreach (ProductRepresentation? item in list)
        {
            result += item.Quantity;
        }

        return result;
    }

    public async Task<int> GetCountGetoutProductRepresentationWithProductId(int productId)
    {
        int result = 0;
        List<ProductRepresentation> list = await GetGetoutProductRepresentationWithProductId(productId);
        foreach (ProductRepresentation? item in list)
        {
            result += item.Quantity;
        }

        return result;
    }

    public async Task<int> GetCountReturnProductRepresentationWithProductId(int productId)
    {
        int result = 0;
        List<ProductRepresentation> list = await GetReturnProductRepresentationWithProductId(productId);
        foreach (ProductRepresentation? item in list)
        {
            result += item.Quantity;
        }

        return result;
    }

    public async Task<int> GetCountEnterManualyProductRepresentationWithProductId(int productId)
    {
        int result = 0;
        List<ProductRepresentation> list = await GetEnterManualyProductRepresentationWithProductId(productId);
        foreach (ProductRepresentation? item in list)
        {
            result += item.Quantity;
        }

        return result;
    }
}