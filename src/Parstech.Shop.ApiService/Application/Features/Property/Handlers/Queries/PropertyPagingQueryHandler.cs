using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Handlers.Queries;

public class PropertyPagingQueryHandler : IRequestHandler<PropertyPagingQueryReq, PagingDto>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;
    private readonly ICateguryRepository _categuryRep;
    private readonly IPropertyCateguryRepository _propertyCateguryRep;

    public PropertyPagingQueryHandler(IPropertyRepository propertyRep,
        IMapper mapper,
        ICateguryRepository categuryRep,
        IPropertyCateguryRepository propertyCateguryRep)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
        _categuryRep = categuryRep;
        _propertyCateguryRep = propertyCateguryRep;
    }

    public async Task<PagingDto> Handle(PropertyPagingQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.Property> properties = await _propertyRep.GetAll();
        IList<PropertyDto> propertyDto = new List<PropertyDto>();
        foreach (Shared.Models.Property item in properties)
        {
            PropertyDto? PDto = _mapper.Map<PropertyDto>(item);

            Shared.Models.Categury? categury = await _categuryRep.GetAsync(item.CateguryId);
            PDto.CateguryTitle = categury.GroupTitle;

            Shared.Models.PropertyCategury? propertycategury =
                await _propertyCateguryRep.GetAsync(item.PropertyCateguryId);
            PDto.PropertyCateguryTitle = propertycategury.Name;
            propertyDto.Add(PDto);
        }

        IQueryable<PropertyDto> result = propertyDto.AsQueryable();
        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                p.Caption.Contains(request.Parameter.Filter));
        }

        if (request.Parameter.categuryId != 0 && request.Parameter.propertyCateguryId != 0)
        {
            List<Shared.Models.Property> propertyies =
                await _propertyRep.GetPropertyBySearch(request.Parameter.categuryId,
                    request.Parameter.propertyCateguryId,
                    null);
            List<PropertyDto>? propertiesDto = _mapper.Map<List<PropertyDto>>(propertyies);
            result = propertiesDto.AsQueryable();
        }

        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;
    }
}