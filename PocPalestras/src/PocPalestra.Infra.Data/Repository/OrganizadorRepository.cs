using PocPalestra.Domain.Organizadores;
using PocPalestra.Domain.Organizadores.Repository;
using PocPalestra.Infra.Data.Context;

namespace PocPalestra.Infra.Data.Repository
{
    public class OrganizadorRepository : Repository<Organizador>, IOrganizadorRepository
    {
        public OrganizadorRepository(PalestraContext context) : base(context)
        {
        }
    }
}
