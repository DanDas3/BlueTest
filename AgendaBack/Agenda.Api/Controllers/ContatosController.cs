using Agenda.Context;
using Agenda.Model;
using Agenda.Service;
using Agenda.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaDbContext _agendaDbContext;

        public ContatoController(AgendaDbContext agendaDbContext)
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
        public async Task<ActionResult<Contato>> Create(Contato contato)
        {
            if (_agendaDbContext.Contatos == null)
            {
                return Problem("Erro no sitema.");
            }
            IValidadorAgenda validadorAgenda = new ValidadorAgenda();
            validadorAgenda.ValidarContato(contato);
            _agendaDbContext.Contatos.Add(contato);
            await _agendaDbContext.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Contato contato)
        {
            if (id != contato.Id)
            {
                return BadRequest();
            }

            _agendaDbContext.Entry(contato).State = EntityState.Modified;

            try
            {
                await _agendaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteContato(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        private bool ExisteContato(int id)
        {
            return (_agendaDbContext.Contatos?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
