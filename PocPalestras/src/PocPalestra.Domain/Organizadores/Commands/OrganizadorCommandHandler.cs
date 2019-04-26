using PocPalestra.Domain.CommandHandlers;
using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Interfaces;
using PocPalestra.Domain.Organizadores.Events;
using PocPalestra.Domain.Organizadores.Repository;
using System.Linq;

namespace PocPalestra.Domain.Organizadores.Commands
{
    public class OrganizadorCommandHandler : CommandHandler,
         IHandler<RegistrarOrganizadorCommand>
    {        
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IBus _bus;

        public OrganizadorCommandHandler(
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications,
            IOrganizadorRepository organizadorRepository, IBus bus) : base(uow, bus, notifications)
        {
            _organizadorRepository = organizadorRepository;
            _bus = bus;
        }
        public void Handle(RegistrarOrganizadorCommand message)
        {
            {
                var organizador = new Organizador(message.Id, message.Nome, message.CPF, message.Email);

                if (!organizador.EhValido())
                {
                    NotificarValidacoesErro(organizador.ValidationResult);
                    return;
                }

                var organizadorExistente = _organizadorRepository.Buscar(o => o.Cpf == organizador.Cpf || o.Email == organizador.Email);

                if (organizadorExistente.Any())
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType, "CPF ou e-mail já utilizados"));
                }

                _organizadorRepository.Adicionar(organizador);

                if (Commit())
                {
                    _bus.RaiseEvent(new OrganizadorRegistradoEvent(organizador.Id, organizador.Nome, organizador.Cpf, organizador.Email));
                }

                return;
            }
        }
    }
}
