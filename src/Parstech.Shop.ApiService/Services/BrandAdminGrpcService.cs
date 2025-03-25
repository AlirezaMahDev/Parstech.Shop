using Grpc.Core;

using MediatR;

using AutoMapper;

using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class BrandAdminGrpcService : BrandAdminService.BrandAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BrandAdminGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<BrandPageingDto> GetBrandsForAdmin(BrandParameterRequest request,
        ServerCallContext context)
    {
        ParameterDto parameter = new()
        {
            CurrentPage = request.CurrentPage, TakePage = request.TakePage, Filter = request.Filter
        };

        var result = await _mediator.Send(new BrandsPagingQueryReq(parameter));
        var response = new BrandPageingDto
        {
            CurrentPage = result.CurrentPage, PageCount = result.PageCount, RowCount = result.RowCount
        };

        foreach (var brand in result.List)
        {
            response.List.Add(MapToBrandDto(brand));
        }

        return response;
    }

    public override async Task<BrandDto> GetBrand(BrandRequest request, ServerCallContext context)
    {
        var brand = await _mediator.Send(new BrandReadCommandReq(request.BrandId));
        return MapToBrandDto(brand);
    }

    public override async Task<ResponseDto> CreateBrand(BrandDto request, ServerCallContext context)
    {
        var brandDto = MapFromBrandDto(request);
        await _mediator.Send(new BrandCreateCommandReq(brandDto));

        return new() { IsSuccessed = true, Message = "برند با موفقیت ثبت شد" };
    }

    public override async Task<ResponseDto> UpdateBrand(BrandDto request, ServerCallContext context)
    {
        var brandDto = MapFromBrandDto(request);
        await _mediator.Send(new BrandUpdateCommandReq(brandDto));

        return new() { IsSuccessed = true, Message = "برند با موفقیت ویرایش شد" };
    }

    public override async Task<ResponseDto> DeleteBrand(BrandRequest request, ServerCallContext context)
    {
        await _mediator.Send(new BrandDeleteCommandReq(request.BrandId));

        return new() { IsSuccessed = true, Message = "برند با موفقیت حذف شد" };
    }

    private BrandDto MapToBrandDto(Shop.Application.DTOs.Brand.BrandDto brand)
    {
        return new()
        {
            BrandId = brand.BrandId,
            BrandTitle = brand.BrandTitle ?? string.Empty,
            LatinBrandTitle = brand.LatinBrandTitle ?? string.Empty,
            BrandImage = brand.BrandImage ?? string.Empty,
            BrandFile = string.Empty, // Since this is an input field in the form, we don't map it when returning data
            ChangeByUserName = brand.ChangeByUserName ?? string.Empty,
            LastChangeTime = brand.LastChangeTime.ToString() ?? string.Empty,
            IsDelete = brand.IsDelete
        };
    }

    private Shop.Application.DTOs.Brand.BrandDto MapFromBrandDto(BrandDto brand)
    {
        return new Shop.Application.DTOs.Brand.BrandDto
        {
            BrandId = brand.BrandId,
            BrandTitle = brand.BrandTitle,
            LatinBrandTitle = brand.LatinBrandTitle,
            BrandImage = brand.BrandImage,
            // BrandFile is not mapped as it's handled differently in the application for file uploads
            ChangeByUserName = brand.ChangeByUserName,
            LastChangeTime =
                !string.IsNullOrEmpty(brand.LastChangeTime) ? DateTime.Parse(brand.LastChangeTime) : DateTime.Now,
            IsDelete = brand.IsDelete
        };
    }
}