using PocPalestra.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace PocPalestra.Domain.Palestras.Repository
{
    public interface IPalestraRepository : IRepository<Palestra>
    {
        IEnumerable<Palestra> ObterPalestraPorOrganizador(Guid organizadorId);
        Endereco ObterEnderecoPorId(Guid id);
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        IEnumerable<Categoria> ObterCategorias();
        Palestra ObterMinhaPalestraPorId(Guid id, Guid organizadorId);
    }
}
