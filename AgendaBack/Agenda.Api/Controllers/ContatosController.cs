using Agenda.Context;
using Agenda.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly AgendaDbContext _agendaDbContext;

        public ContatosController(AgendaDbContext agendaDbContext)
        {
            _agendaDbContext = agendaDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatosAsync()
        {
            if (_agendaDbContext.Contatos == null)
            {
                return NotFound();
            }
            return await _agendaDbContext.Contatos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            if (_agendaDbContext.Contatos == null)
            {
                return NotFound();
            }
            var evento = await _agendaDbContext.Contatos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> Create(Contato evento)
        {
            if (_agendaDbContext.Contatos == null)
            {
                return Problem("Entity set 'AgendaDbContext.Contatos' null.");
            }
            _agendaDbContext.Contatos.Add(evento);
            await _agendaDbContext.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = evento.Id }, evento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_agendaDbContext.Contatos == null)
            {
                return NotFound();
            }
            var cliente = await _agendaDbContext.Contatos.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _agendaDbContext.Contatos.Remove(cliente);
            await _agendaDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
