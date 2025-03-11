using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IPropertyRepository:IGenericRepository<Property>
    {
        Task<List<Property>>GetPropertyBySearch(int categuryId,int propertyCateguryId, string filter);

        Task<List<Property>> GetPropertiesOfCategory (int categoryId);
        Task<bool> ExistProperty (string propertyName);
        Task<Property?> GetByName(string propertyName);
        Task<bool> ExistPropertyForCateguryId(int categuryId);
    }
}
