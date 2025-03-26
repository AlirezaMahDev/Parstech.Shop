using Parstech.Shop.Context.Application.DTOs.TicketDetails;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
{
    Task<IQueryable<TicketDetailsDto>> GetTicketDetailOfTicketWithTypeTitle(int ticketId);
}