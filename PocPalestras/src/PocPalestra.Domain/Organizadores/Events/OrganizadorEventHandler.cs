using PocPalestra.Domain.Core.Events;
using System;

namespace PocPalestra.Domain.Organizadores.Events
{
    public class OrganizadorEventHandler :
        IHandler<OrganizadorRegistradoEvent>
    {      
        public void Handle(OrganizadorRegistradoEvent message)
        {
            // TODO: Enviar um email?
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Palestra atualizado com sucesso");
        }
    }
}
