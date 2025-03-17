using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class CategoryGrpcService : CategoryService.CategoryServiceBase
{
    private readonly IMediator _mediator;

    public CategoryGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CategoryResponse> GetParentCategories(ParentCategoriesRequest request,
        ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new ParentCategoriesQueryReq());

            var response = new CategoryResponse();
            foreach (var category in categories)
            {
                response.Categories.Add(MapCategoryToProto(category));
            }

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
            var categories = await _mediator.Send(new CategorySubesQueryReq(request.ParentId));

            var response = new CategoryResponse();
            foreach (var category in categories)
            {
                response.Categories.Add(MapCategoryToProto(category));
            }

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
            var category = await _mediator.Send(new GetCategoryByLatinNameQueryReq(request.LatinName));

            return MapCategoryToProto(category);
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<CategoriesMenuResponse> GetCategoriesMenu(CategoriesMenuRequest request,
        ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new ShowCateguriesQueryReq());

            var response = new CategoriesMenuResponse();
            foreach (var parentCategory in categories)
            {
                var parent = new CategoryMenuParent
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    LatinName = parentCategory.LatinName,
                    Image = parentCategory.Img ?? string.Empty
                };

                // Add children
                foreach (var childCategory in parentCategory.Childs)
                {
                    var child = new CategoryMenuChild
                    {
                        Id = childCategory.Id, Name = childCategory.Name, LatinName = childCategory.LatinName
                    };

                    // Add grandchildren
                    foreach (var grandChildCategory in childCategory.Childs)
                    {
                        child.GrandChildren.Add(new CategoryMenuGrandChild
                        {
                            Id = grandChildCategory.Id,
                            Name = grandChildCategory.Name,
                            LatinName = grandChildCategory.LatinName
                        });
                    }

                    parent.Children.Add(child);
                }

                response.Parents.Add(parent);
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    private Category MapCategoryToProto(Shop.Application.DTOs.Categories.CategoryDto categoryDto)
    {
        var category = new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name ?? string.Empty,
            LatinName = categoryDto.LatinName ?? string.Empty,
            Description = categoryDto.Description ?? string.Empty,
            Image = categoryDto.Image ?? string.Empty,
            ParentId = categoryDto.ParentId,
            GroupId = categoryDto.GroupId,
            IsActive = categoryDto.IsActive,
            Order = categoryDto.Order
        };

        if (categoryDto.Subes != null)
        {
            foreach (var sub in categoryDto.Subes)
            {
                category.SubCategories.Add(MapCategoryToProto(sub));
            }
        }

        return category;
    }
}