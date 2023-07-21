using Agenda.Model;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Context
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
    }
}