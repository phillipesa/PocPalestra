using PocPalestra.Domain.Core.Commands;
using PocPalestra.Domain.Interfaces;
using PocPalestra.Infra.Data.Context;

namespace PocPalestra.Infra.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PalestraContext _context;

        public UnitOfWork(PalestraContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
