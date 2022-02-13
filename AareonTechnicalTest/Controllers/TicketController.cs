using AareonTechnicalTest.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AareonTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controler]")]
    public class TicketController : ControllerBase
    {
        [HttpGet(Name = "GetTicket")]
        public TicketDto getTicket(int id)
        {
            return null;
        }

        [HttpPost(Name = "SaveTicket")]
        public int saveTicket(TicketDto ticketDto)
        {
            return 0;
        }

        [HttpPut(Name = "UpdateTicket")]
        public TicketDto updateTicket(TicketDto ticketDto)
        {
            return null;
        }

        [HttpDelete(Name = "DeleteTicket")]
        public bool deleteTicket(TicketDto ticketDto)
        {
            return false;
        }
    }
}
