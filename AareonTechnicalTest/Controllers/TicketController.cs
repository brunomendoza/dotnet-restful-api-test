using AareonTechnicalTest.DTO;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TicketController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return TicketToDto(ticket);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            return await _context.Tickets
                .Select(t => TicketToDto(t))
                .ToListAsync();
        }

        [HttpPost(Name = "SaveTicket")]
        public async Task<ActionResult<TicketDto>> SaveTicket(TicketDto ticketDto)
        {
            Ticket ticket = new()
            {
                PersonId = ticketDto.Id,
                Content = ticketDto.Content
            };

            await _context.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, TicketToDto(ticket));
        }

        //[HttpPut(Name = "UpdateTicket")]
        //public TicketDto UpdateTicket(TicketDto ticketDto)
        //{
        //    return null;
        //}

        //[HttpDelete(Name = "DeleteTicket")]
        //public bool DeleteTicket(TicketDto ticketDto)
        //{
        //    return false;
        //}

        private static TicketDto TicketToDto(Ticket ticket)
        {
            return new TicketDto
            {
                Id = ticket.Id,
                Content = ticket.Content,
                PersonId = ticket.PersonId
            };
        }
    }
}
