using AutoMapper;
using Shop.Application.DTOs.Status;
using Shop.Application.DTOs.Tax;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
	public class StatusMapping:Profile
	{
		public StatusMapping()
		{
			CreateMap<Status, StatusDto>().ReverseMap();
		}
	}
}
