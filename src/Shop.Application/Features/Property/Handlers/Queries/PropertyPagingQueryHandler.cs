using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Property;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.Property.Requests.Queries;
using Shop.Application.Features.PropertyCategury.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Application.Features.Property.Handlers.Queries
{
    public class PropertyPagingQueryHandler : IRequestHandler<PropertyPagingQueryReq, PagingDto>
    {
        private readonly IPropertyRepository _propertyRep;
        private readonly IMapper _mapper;
        private readonly ICateguryRepository _categuryRep;
        private readonly IPropertyCateguryRepository _propertyCateguryRep;

        public PropertyPagingQueryHandler(IPropertyRepository propertyRep,
            IMapper mapper, ICateguryRepository categuryRep,
            IPropertyCateguryRepository propertyCateguryRep)
        {
            _propertyRep = propertyRep;
            _mapper = mapper;
            _categuryRep = categuryRep;
            _propertyCateguryRep = propertyCateguryRep;
        }
        public async Task<PagingDto> Handle(PropertyPagingQueryReq request, CancellationToken cancellationToken)
        {

            var properties = await _propertyRep.GetAll();
            IList<PropertyDto> propertyDto = new List<PropertyDto>();
            foreach (var item in properties)
            {
                var PDto = _mapper.Map<PropertyDto>(item);

                var categury = await _categuryRep.GetAsync(item.CateguryId);
                PDto.CateguryTitle = categury.GroupTitle;

                var propertycategury = await _propertyCateguryRep.GetAsync(item.PropertyCateguryId);
                PDto.PropertyCateguryTitle = propertycategury.Name;
                propertyDto.Add(PDto);
            }

            IQueryable<PropertyDto> result = propertyDto.AsQueryable();
            PagingDto response = new PagingDto();

            if (!string.IsNullOrEmpty(request.Parameter.Filter))
            {
                result = result.Where(p =>
                (p.Caption.Contains(request.Parameter.Filter)));
            }

            if (request.Parameter.categuryId != 0 && request.Parameter.propertyCateguryId != 0)
            {
                var propertyies = await _propertyRep.GetPropertyBySearch(request.Parameter.categuryId, request.Parameter.propertyCateguryId,null);
                var propertiesDto = _mapper.Map<List<PropertyDto>>(propertyies);
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
}
