using PocPalestra.Domain.Core.Events;
using System;

namespace PocPalestra.Domain.Palestras.Events
{
    public class PalestraEventHandler :
        IHandler<PalestraRegistradaEvent>,
        IHandler<PalestraAtualizadaEvent>,
        IHandler<PalestraExcluidaEvent>,
        IHandler<EnderecoPalestraAtualizadoEvent>,
        IHandler<EnderecoPalestraAdicionadoEvent>
    {
        public void Handle(PalestraRegistradaEvent message)
        {
            // Enviar um email ou fila
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Palestra resgistrado com sucesso");
        }

        public void Handle(PalestraAtualizadaEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Palestra atualizado com sucesso");
        }

        public void Handle(PalestraExcluidaEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Palestra excluido com sucesso");
        }

        public void Handle(EnderecoPalestraAtualizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Edereço da palestra atualizado com sucesso");
        }

        public void Handle(EnderecoPalestraAdicionadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Edereço da palestra registrado com sucesso");
        }      
    }
}
