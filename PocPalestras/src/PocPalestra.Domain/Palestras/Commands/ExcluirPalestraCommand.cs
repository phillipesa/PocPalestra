using PocPalestra.Domain.Core.Commands;
using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public class ExcluirPalestraCommand : BasePalestraCommand
    {
        public ExcluirPalestraCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
