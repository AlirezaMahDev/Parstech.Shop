using AutoMapper;
using Shop.Application.DTOs.Ticket;
using Shop.Application.DTOs.TicketDetails;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class TicketDetailsMapping : Profile
    {
        public TicketDetailsMapping()
        {
            CreateMap<TicketDetail, TicketDetailsDto>().ReverseMap();
        }
    }
}
