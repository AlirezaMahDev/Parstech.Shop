﻿using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.PropertyCategury;
using Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.PropertyCategury.Handlers.Queries;

public class PropertyCateguryPagingQueryHandler : IRequestHandler<PropertyCateguryPagingQueryReq, PagingDto>
{
    private readonly IPropertyCateguryRepository _propertyCatRep;
    private readonly IMapper _mapper;

    public PropertyCateguryPagingQueryHandler(IPropertyCateguryRepository propertyCatRep,
        IMapper mapper)
    {
        _propertyCatRep = propertyCatRep;
        _mapper = mapper;
    }
    public async Task<PagingDto> Handle(PropertyCateguryPagingQueryReq request, CancellationToken cancellationToken)
    {

        var PropCats = await _propertyCatRep.GetAll();
        IList<PropertyCateguryDto> categuryDto = new List<PropertyCateguryDto>();
        foreach (var item in PropCats)
        {
            var PDto = _mapper.Map<PropertyCateguryDto>(item);
            categuryDto.Add(PDto);
        }

        IQueryable<PropertyCateguryDto> result = categuryDto.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                (p.Name.Contains(request.Parameter.Filter)));
        }
        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;

    }
}