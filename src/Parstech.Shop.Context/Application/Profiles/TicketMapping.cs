using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.Ticket;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class TicketMapping : Profile
{
    public TicketMapping()
    {
        CreateMap<Ticket, TicketDto>().ReverseMap();
    }
}