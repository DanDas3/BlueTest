using Agenda.Model;
using Agenda.Service.Exceptions;
using Agenda.Service.Interfaces;
using System;

namespace Agenda.Service.Test
{
    [TestFixture]
    public class ValidadorAgendaTests
    {
        private IValidadorAgenda _validador;
        private Contato _contato;
        [SetUp]
        public void Setup()
        {
            _validador = new ValidadorAgenda();
            _contato = new Contato();
        }

        [Test]
        public void ValidarContato_ChamadoComObjetoContatoNull_LancaExcecaoTipoAgendaException()
        {
            Assert.Throws(Is.TypeOf<AgendaException>()
                .And.Message.EqualTo("Objeto contato nulo."),
              () => _validador.ValidarContato(null));
        }

        [Test]
        public void ValidarContato_ChamadoComPropedadeNomeNullOuVazio_LancaExcecaoTipoAgendaException()
        {
            Assert.Throws(Is.TypeOf<AgendaException>()
                .And.Message.EqualTo("Nome do contato vazio."),
              () => _validador.ValidarContato(_contato));
        }

        [Test]
        public void ValidarContato_ChamadoComPropedadeTelefoneNullOuVazio_LancaExcecaoTipoAgendaException()
        {
            _contato.Nome = "teste";
            Assert.Throws(Is.TypeOf<AgendaException>()
                .And.Message.EqualTo("Telefone do contato vazio."),
              () => _validador.ValidarContato(_contato));
        }

        [Test]
        public void ValidarContato_ChamadoComPropedadeTelefoneComLetras_LancaExcecaoTipoAgendaException()
        {
            _contato.Nome = "teste";
            _contato.Telefone = "aaa";
            Assert.Throws(Is.TypeOf<AgendaException>()
                .And.Message.EqualTo("Telefone do contato invalido."),
              () => _validador.ValidarContato(_contato));
        }

        [Test]
        public void ValidarContato_ChamadoComPropedadeEmailForaPadrao_LancaExcecaoTipoAgendaException()
        {
            _contato.Nome = "teste";
            _contato.Telefone = "12345678900";
            _contato.Email = "email";
            Assert.Throws(Is.TypeOf<AgendaException>()
                .And.Message.EqualTo("Email informado não é um email válido."),
              () => _validador.ValidarContato(_contato));
        }
    }
}