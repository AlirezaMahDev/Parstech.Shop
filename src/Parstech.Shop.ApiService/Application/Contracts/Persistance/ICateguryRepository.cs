using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface ICateguryRepository:IGenericRepository<Categury>
    {
        Task<List<Categury>> GetCateguryByParentId(int parentId, int? Row);
        Task<List<Categury>> GetAllParentCategury();
        
        Task<List<Categury>> GetShowCateguryByParentId(int parentId);

        Task<Categury?> GetCateguryByLatinName(string latinName);
        Task<Categury?> GetCateguryByName(string name);
       
    }
}
