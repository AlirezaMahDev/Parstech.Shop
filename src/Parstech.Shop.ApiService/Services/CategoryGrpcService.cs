using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.Category;
using Shop.Application.Features.Category.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class CategoryGrpcService : CategoryService.CategoryServiceBase
    {
        private readonly IMediator _mediator;
        
        public CategoryGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<CategoryResponse> GetParentCategories(ParentCategoriesRequest request, ServerCallContext context)
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
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<CategoryResponse> GetSubCategories(SubCategoriesRequest request, ServerCallContext context)
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
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<Category> GetCategoryByLatinName(CategoryByLatinNameRequest request, ServerCallContext context)
        {
            try
            {
                var category = await _mediator.Send(new GetCategoryByLatinNameQueryReq(request.LatinName));
                
                return MapCategoryToProto(category);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
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
} 