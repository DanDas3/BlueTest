using Agenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Service.Interfaces
{
    public interface IValidadorAgenda
    {
        public void ValidarContato(Contato contato);
    }
}
