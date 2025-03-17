using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class CategoryService : CategoryServiceBase
{
    private readonly IMediator _mediator;
    private readonly ICateguryRepository _categoryRepository;

    public CategoryService(IMediator mediator, ICateguryRepository categoryRepository)
    {
        _mediator = mediator;
        _categoryRepository = categoryRepository;
    }

    public override async Task<CategoryResponse> GetParentCategories(ParentCategoriesRequest request,
        ServerCallContext context)
    {
        try
        {
            var query = new CateguryParentsReadQueryReq();
            var categories = await _mediator.Send(query);

            var response = new CategoryResponse();
            response.Categories.AddRange(categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                LatinName = c.LatinName,
                Description = c.Description,
                Image = c.Image,
                ParentId = c.ParentId,
                GroupId = c.GroupId,
                IsActive = c.IsActive,
                Order = c.Order
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<CategoryResponse> GetSubCategories(SubCategoriesRequest request,
        ServerCallContext context)
    {
        try
        {
            var query = new CateguryByParentIdReadQueryReq(request.ParentId);
            var categories = await _mediator.Send(query);

            var response = new CategoryResponse();
            response.Categories.AddRange(categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                LatinName = c.LatinName,
                Description = c.Description,
                Image = c.Image,
                ParentId = c.ParentId,
                GroupId = c.GroupId,
                IsActive = c.IsActive,
                Order = c.Order
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<Category> GetCategoryByLatinName(CategoryByLatinNameRequest request,
        ServerCallContext context)
    {
        try
        {
            var query = new GetCateguryByLatinameQueryReq(request.LatinName);
            var category = await _mediator.Send(query);

            return new()
            {
                Id = category.Id,
                Name = category.Name,
                LatinName = category.LatinName,
                Description = category.Description,
                Image = category.Image,
                ParentId = category.ParentId,
                GroupId = category.GroupId,
                IsActive = category.IsActive,
                Order = category.Order
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}