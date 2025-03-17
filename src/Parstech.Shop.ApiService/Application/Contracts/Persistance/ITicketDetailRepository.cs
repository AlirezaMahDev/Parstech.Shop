using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
{
    Task<IQueryable<TicketDetailsDto>> GetTicketDetailOfTicketWithTypeTitle(int ticketId);
}