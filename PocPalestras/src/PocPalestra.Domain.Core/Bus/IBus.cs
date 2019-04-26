using PocPalestra.Domain.Core.Commands;
using PocPalestra.Domain.Core.Events;

namespace PocPalestra.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
