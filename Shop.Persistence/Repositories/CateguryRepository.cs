using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class CateguryRepository:GenericRepository<Categury>,ICateguryRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CateguryRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Categury>> GetCateguryByParentId(int parentId,int? Row)
        {
            if (parentId==0)
            {
                return await _context.Categuries.Where(u => u.ParentId == null).ToListAsync();
            }
            else
            {
                if (Row != null)
                {
                    return await _context.Categuries.Where(u => u.ParentId == parentId && u.Row == Row).ToListAsync();
                }
                else
                {
                    return await _context.Categuries.Where(u => u.ParentId == parentId ).ToListAsync();
                }
            }
        }

        

        public async Task<List<Categury>> GetAllParentCategury()
        {
            return await _context.Categuries.Where(u => u.IsParnet ).ToListAsync();
        }


        public async Task<List<Categury>> GetShowCateguryByParentId(int parentId)
        {
            if (parentId==0)
            {
                return await _context.Categuries.Where(u =>u.Show&& u.ParentId == null).ToListAsync();
            }
            else
            {
                return await _context.Categuries.Where(u =>u.Show&& u.ParentId == parentId).ToListAsync();
            }
        }

        public async Task<Categury?> GetCateguryByLatinName(string latinName)
        {
            return await _context.Categuries.FirstOrDefaultAsync(u => u.LatinGroupTitle == latinName);
        }
        public async Task<Categury?> GetCateguryByName(string name)
        {
            return await _context.Categuries.FirstOrDefaultAsync(u => u.GroupTitle==name);
        }
        
        
        
    }
}
