using Agenda.Context;
using Agenda.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly AgendaDbContext _agendaDbContext;

        public EventosController(AgendaDbContext agendaDbContext)
        {
            _agendaDbContext = agendaDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventosAsync()
        {
            if (_agendaDbContext.Eventos == null)
            {
                return NotFound();
            }
            return await _agendaDbContext.Eventos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            if (_agendaDbContext.Eventos == null)
            {
                return NotFound();
            }
            var evento = await _agendaDbContext.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Create(Evento evento)
        {
            if (_agendaDbContext.Eventos == null)
            {
                return Problem("Entity set 'AgendaDbContext.Eventos' null.");
            }
            _agendaDbContext.Eventos.Add(evento);
            await _agendaDbContext.SaveChangesAsync();

            return CreatedAtAction("GetEvento", new { id = evento.Id }, evento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_agendaDbContext.Eventos == null)
            {
                return NotFound();
            }
            var cliente = await _agendaDbContext.Eventos.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _agendaDbContext.Eventos.Remove(cliente);
            await _agendaDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
