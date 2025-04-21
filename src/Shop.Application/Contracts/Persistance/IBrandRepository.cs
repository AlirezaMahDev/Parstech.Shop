using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IBrandRepository:IGenericRepository<Brand>
    {
        int GetCountOfBrands();
        Task<Brand?> GetByName(string name);
    }
}
