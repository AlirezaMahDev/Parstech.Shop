using AutoMapper;
using Shop.Application.DTOs.Tax;
using Shop.Application.DTOs.Ticket;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class TicketMapping : Profile
    {
        public TicketMapping()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
        }
    }
}
