using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Ticket;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {

    }
}
