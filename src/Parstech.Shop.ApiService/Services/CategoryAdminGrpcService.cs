using Grpc.Core;

using MediatR;

using AutoMapper;

using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class CategoryAdminGrpcService : CategoryAdminService.CategoryAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryAdminGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<CategoryPageingDto> GetCategoriesForAdmin(CategoryParameterRequest request,
        ServerCallContext context)
    {
        ParameterDto parameter = new()
        {
            CurrentPage = request.CurrentPage, TakePage = request.TakePage, Filter = request.Filter
        };

        var result = await _mediator.Send(new CateguryPagingQueryReq(parameter));
        var response = new CategoryPageingDto
        {
            CurrentPage = result.CurrentPage, PageCount = result.PageCount, RowCount = result.RowCount
        };

        foreach (var category in result.List)
        {
            response.List.Add(MapToCategoryDto(category));
        }

        return response;
    }

    public override async Task<CategoryDto> GetCategory(CategoryRequest request, ServerCallContext context)
    {
        var category = await _mediator.Send(new CateguryOneReadCommandReq(request.CategoryId));
        return MapToCategoryDto(category);
    }

    public override async Task<CategoryListResponse> GetCategoryParents(EmptyRequest request, ServerCallContext context)
    {
        var parents = await _mediator.Send(new CateguryParentsReadQueryReq());
        var response = new CategoryListResponse();

        foreach (var parent in parents)
        {
            response.Categories.Add(MapToCategoryDto(parent));
        }

        return response;
    }

    public override async Task<CategoryListResponse> GetAllCategories(CategoryFilterRequest request,
        ServerCallContext context)
    {
        var categories = await _mediator.Send(new CateguryReadCommandReq(request.Filter));
        var response = new CategoryListResponse();

        foreach (var category in categories)
        {
            response.Categories.Add(MapToCategoryDto(category));
        }

        return response;
    }

    public override async Task<ResponseDto> CreateCategory(CategoryDto request, ServerCallContext context)
    {
        var categoryDto = MapFromCategoryDto(request);
        await _mediator.Send(new CateguryCreateCommandReq(categoryDto));

        return new() { IsSuccessed = true, Message = "دسته بندی با موفقیت ثبت شد" };
    }

    public override async Task<ResponseDto> UpdateCategory(CategoryDto request, ServerCallContext context)
    {
        var categoryDto = MapFromCategoryDto(request);
        await _mediator.Send(new CateguryUpdateCommandReq(categoryDto));

        return new() { IsSuccessed = true, Message = "دسته بندی با موفقیت ویرایش شد" };
    }

    public override async Task<ResponseDto> DeleteCategory(CategoryRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(new CateguryDeleteCommandReq(request.CategoryId));

        return new()
        {
            IsSuccessed = response.IsSuccessed,
            Message = response.Message,
            Object = response.Object?.ToString() ?? string.Empty
        };
    }

    private CategoryDto MapToCategoryDto(Shop.Application.DTOs.Categury.CateguryDto category)
    {
        return new CategoryDto
        {
            GroupId = category.GroupId,
            GroupTitle = category.GroupTitle ?? string.Empty,
            LatinGroupTitle = category.LatinGroupTitle ?? string.Empty,
            ParentId = category.ParentId,
            Image = category.Image ?? string.Empty,
            IsParnet = category.IsParnet,
            Show = category.Show
        };
    }

    private Parstech.Shop.Application.DTOs.Categury.CateguryDto MapFromCategoryDto(CategoryDto category)
    {
        return new Parstech.Shop.Application.DTOs.Categury.CateguryDto
        {
            GroupId = category.GroupId,
            GroupTitle = category.GroupTitle,
            LatinGroupTitle = category.LatinGroupTitle,
            ParentId = category.ParentId,
            Image = category.Image,
            IsParnet = category.IsParnet,
            Show = category.Show
        };
    }
}