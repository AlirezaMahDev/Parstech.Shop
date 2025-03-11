using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class OrderShippingRepository : GenericRepository<OrderShipping>, IOrderShippingRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderShippingRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderShipping> GetOrderShippingByOrderId(int orderId)
        {
            OrderShipping result = new OrderShipping();
            if (await _context.OrderShippings.AnyAsync(a => a.OrderId == orderId))
            {
                result = await _context.OrderShippings.FirstOrDefaultAsync(a => a.OrderId == orderId);
            }
            return result;
        }


    }
}
