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
        private readonly ApplicationContext dbContext;

        public TicketController(ApplicationContext context)
        {
            dbContext = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            Ticket ticket = await dbContext.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return TicketToDto(ticket);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            return await dbContext.Tickets
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

            await dbContext.AddAsync(ticket);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, TicketToDto(ticket));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketDto ticketDto)
        {
            if (id != ticketDto.Id)
            {
                return BadRequest();
            }

            Ticket ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Content = ticketDto.Content;
            ticket.PersonId = ticketDto.PersonId;

            try
            {
                await dbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ) when (!TicketExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            Ticket ticket = await dbContext.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            dbContext.Tickets.Remove(ticket);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return dbContext.Tickets.Any(t => t.Id == id);
        }
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
