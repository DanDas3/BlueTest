using Agenda.Model;
using Agenda.Service.Exceptions;
using Agenda.Service.Interfaces;
using System.Text.RegularExpressions;

namespace Agenda.Service
{
    public class ValidadorAgenda : IValidadorAgenda
    {
        public ValidadorAgenda()
        {
            
        }

        public void ValidarContato(Contato contato)
        {
            ValidarObjeto(contato);

            ValidadarNome(contato.Nome);

            ValidadarTelefone(contato.Telefone);

            ValidadarEmail(contato.Email);
        }

        private void ValidadarEmail(string email)
        {
            Regex emailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            
            if (string.IsNullOrWhiteSpace(email)) 
            {
                throw new ArgumentNullException();
            }
            else if (!emailRegex.IsMatch(email))
            {
                throw new AgendaException("Email informado não é um email válido.");
            }
        }

        private void ValidadarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                throw new AgendaException("Telefone do contato vazio.");
            }
            else if (telefone.Any(Char.IsLetter))
            {
                throw new AgendaException("Telefone do contato invalido.");
            }
        }

        private void ValidadarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new AgendaException("Nome do contato vazio.");
            }
        }

        private void ValidarObjeto(Contato contato) 
        {
            if (contato == null)
            {
                throw new AgendaException("Objeto contato nulo.");
            }
        }
    }
}