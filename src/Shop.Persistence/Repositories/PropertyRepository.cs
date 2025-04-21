using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class PropertyRepository:GenericRepository<Property>,IPropertyRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public PropertyRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Property>> GetPropertyBySearch(int categuryId, int propertyCateguryId,string filter)
        {
            IQueryable<Property> properties = _context.Properties;
            List<Property> result = new List<Property>();
            if (filter !=null)
            {
                properties = properties.Where(u => u.Caption.Contains(filter));
            }
            if (categuryId != 0)
            {
                properties = properties.Where(u => u.CateguryId == categuryId);
            }
            if (propertyCateguryId != 0)
            {
                properties = properties.Where(u => u.PropertyCateguryId == propertyCateguryId);
            }
            
            
            result =await properties.ToListAsync();
            return result;
        }


        

        public async Task<List<Property>> GetPropertiesOfCategory(int categoryId)
        {
            return await _context.Properties.Where(z => z.CateguryId == categoryId).ToListAsync();
        }

        public async Task<bool> ExistProperty(string propertyName)
        {
            if(await _context.Properties.AnyAsync(u=>u.Caption==propertyName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Property?> GetByName(string propertyName)
        {
            var result = new Property();
            if(await _context.Properties.AnyAsync(u=>u.Caption==propertyName))
            {
                
                return await _context.Properties.FirstOrDefaultAsync(u => u.Caption == propertyName);
            }
            else
            {
                return result;
            }
            
        }

        public async Task<bool> ExistPropertyForCateguryId(int categuryId)
        {
            if (await _context.Properties.AnyAsync(u => u.CateguryId == categuryId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
