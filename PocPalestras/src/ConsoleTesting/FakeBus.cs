using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Commands;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Palestras.Commands;
using PocPalestra.Domain.Palestras.Events;
using System;

namespace ConsoleTesting
{
    public class FakeBus : IBus
    {
        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"Evento {theEvent.MessageType} lançado");
            Publish(theEvent);
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Command {theCommand.MessageType} lançado");
            Publish(theCommand);
        }

        private static void Publish<T>(T message) where T : Message
        {
            var msgType = message.MessageType;
            if (msgType.Equals("DomainNotification"))
            {
                var obj = new DomainNotificationHandler();
                ((IDomainNotificationHandler<T>)obj).Handle(message);
            }
            if (msgType.Equals("RegistrarPalestraCommand") ||
                msgType.Equals("ExcluirPalestraCommand") ||
                msgType.Equals("AtualizarPalestraCommand"))
            {
                var obj = new PalestraCommandHandler(new FakeRepository(), new FakeUow(), new FakeBus(), new DomainNotificationHandler());
                ((IHandler<T>)obj).Handle(message);
            }

            if (msgType.Equals("PalestraRegistradaEvent") ||
                msgType.Equals("PalestraExcluidaEvent") ||
                msgType.Equals("PalestraAtualizadaEvent"))
            {
                var obj = new PalestraEventHandler();
                ((IHandler<T>)obj).Handle(message);
            }
        }
    }
}
