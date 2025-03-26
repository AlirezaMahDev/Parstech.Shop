using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.TicketDetails;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class TicketDetailsMapping : Profile
{
    public TicketDetailsMapping()
    {
        CreateMap<TicketDetail, TicketDetailsDto>().ReverseMap();
    }
}