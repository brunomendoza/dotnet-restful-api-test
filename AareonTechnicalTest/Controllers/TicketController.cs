using AareonTechnicalTest.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AareonTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controler]")]
    public class TicketController : ControllerBase
    {
        [HttpGet(Name = "Ticket")]
        public TicketDto GetTicket(int id)
        {
            return null;
        }

        [HttpGet(Name = "Tickets")]
        public IEnumerable<TicketDto> GetAllTickets()
        {
            ApplicationContext applicationContext = new ApplicationContext(new DbContextOptions<ApplicationContext>());
            List<TicketDto> tickets = new System.Collections.Generic.List<TicketDto>();

            foreach (Models.Ticket ticket in applicationContext.Tickets)
            {
                tickets.Add(new TicketDto());
            }

            return tickets;
        }

        [HttpPost(Name = "SaveTicket")]
        public int SaveTicket(TicketDto ticketDto)
        {
            return 0;
        }

        [HttpPut(Name = "UpdateTicket")]
        public TicketDto UpdateTicket(TicketDto ticketDto)
        {
            return null;
        }

        [HttpDelete(Name = "DeleteTicket")]
        public bool DeleteTicket(TicketDto ticketDto)
        {
            return false;
        }
    }
}
