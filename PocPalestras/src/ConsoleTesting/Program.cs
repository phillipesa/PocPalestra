using PocPalestra.Domain.CommandHandlers;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Domain.Palestras.Commands;
using System;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new FakeBus();

            // Registro com sucesso
            var endereco = new IncluirEnderecoPalestraCommand(Guid.NewGuid(),"","","","","","","", Guid.NewGuid());
            var cmd = new RegistrarPalestraCommand("Dexter","Teste","Tesste Testes", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Supermix",Guid.NewGuid(),Guid.NewGuid(),endereco);
            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            // Registro com erros
            cmd = new RegistrarPalestraCommand("", "Teste", "Tesste Testes", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), false, 0, false, "", Guid.NewGuid(), Guid.NewGuid(), endereco);
            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            // Atualizar palestra
            var cmd2 = new AtualizarPalestraCommand(Guid.NewGuid(), "Dexter", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Supermix", "Teste", "Teste de sistema");
            Inicio(cmd2);
            bus.SendCommand(cmd2);
            Fim(cmd2);

            var cmd3 = new ExcluirPalestraCommand(Guid.NewGuid());
            Inicio(cmd3);
            bus.SendCommand(cmd3);
            Fim(cmd3);

            Console.ReadKey();
        }

        private static void Inicio(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Inicio do commando " + message.MessageType);
        }

        private static void Fim(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Fim do commando " + message.MessageType);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************");
            Console.WriteLine("");            
        }
    }
}
