using System;

namespace PocPalestra.Domain.Palestras.Events
{
    public class PalestraExcluidaEvent : BasePalestraEvent
    {
        public PalestraExcluidaEvent(Guid id)
        {
            Id = id;            
            AggregateId = id;
        }
    }
}