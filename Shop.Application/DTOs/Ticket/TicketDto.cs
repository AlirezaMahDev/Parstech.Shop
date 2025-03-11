using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Ticket
{
    public class TicketDto
    {
        public int TicketId { get; set; }

        public int StatusId { get; set; }

        public string StatusTitle { get; set; }

        public int UserId { get; set; }

        public int DepartmentId { get; set; }

        public string TicketCaption { get; set; } = null!;

        public DateTime CreateDate { get; set; }
    }
}
