using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductPropertyRepository:IGenericRepository<ProductProperty>
    {
        Task<ProductProperty?> GetpropertyByProduct(int productId);
        Task<List<ProductProperty>> GetPropertiesByProduct(int productId);
        Task<bool> ExistPropertyForProduct(int productId, int propertyId);
    }
}
