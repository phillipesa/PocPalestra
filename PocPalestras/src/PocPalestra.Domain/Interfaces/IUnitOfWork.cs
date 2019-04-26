using PocPalestra.Domain.Core.Commands;
using System;

namespace PocPalestra.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
