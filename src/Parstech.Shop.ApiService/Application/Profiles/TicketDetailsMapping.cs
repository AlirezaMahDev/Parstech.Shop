using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class TicketDetailsMapping : Profile
{
    public TicketDetailsMapping()
    {
        CreateMap<TicketDetail, TicketDetailsDto>().ReverseMap();
    }
}