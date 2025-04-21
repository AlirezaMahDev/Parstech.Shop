using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class BrandRepository:GenericRepository<Brand>,IBrandRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public BrandRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public int GetCountOfBrands()
        {

            return _context.Brands.Count();
        }
        public async Task<Brand?> GetByName(string name)
        {

            return await _context.Brands.FirstOrDefaultAsync(u=>u.BrandTitle==name);
        }
    }
}
