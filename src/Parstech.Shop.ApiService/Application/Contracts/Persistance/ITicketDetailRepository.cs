using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
{
    Task<IQueryable<TicketDetailsDto>> GetTicketDetailOfTicketWithTypeTitle(int ticketId);
}