using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IPropertyCateguryRepository:IGenericRepository<PropertyCategury>
    {

       
    }
}
