using PocPalestra.Domain.Core.Commands;
using PocPalestra.Domain.Interfaces;
using System;

namespace ConsoleTesting
{
    public class FakeUow : IUnitOfWork
    {
        public FakeUow()
        {
        }

        public CommandResponse Commit()
        {
            return new CommandResponse(true);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
