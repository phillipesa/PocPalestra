using PocPalestra.Domain.Core.Events;

namespace PocPalestra.Domain.Interfaces
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}
