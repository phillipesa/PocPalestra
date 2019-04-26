using FluentValidation.Results;
using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Interfaces;
using System;

namespace PocPalestra.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        public CommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = notifications;
        }
        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
                _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }
        protected bool Commit()
        {
            // TODO: há agluma validação de negocio com erro
            if (_notifications.HasNotifications()) return false;

            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            Console.WriteLine("Erro ao salvar");
            _bus.RaiseEvent(new DomainNotification("Commmit", "Erro ao salvar"));

            return false;
        }
    }
}
