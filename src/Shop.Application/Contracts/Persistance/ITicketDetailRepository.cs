using Shop.Application.DTOs.Ticket;
using Shop.Application.DTOs.TicketDetails;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
    {
        Task<IQueryable<TicketDetailsDto>> GetTicketDetailOfTicketWithTypeTitle(int ticketId);
    }
}
