using PocPalestra.Domain.Palestras;
using PocPalestra.Domain.Palestras.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleTesting
{
    public class FakeRepository : IPalestraRepository
    {
        public void Adicionar(Palestra obj)
        {
            //
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Palestra obj)
        {
            //
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Palestra> Buscar(Expression<Func<Palestra, bool>> predicade)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            throw new NotImplementedException();
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {            
            return new Endereco(Guid.NewGuid(), "", "", "", "", "", "", "", Guid.NewGuid());            
        }

        public Palestra ObterMinhaPalestraPorId(Guid id, Guid organizadorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Palestra> ObterPalestraPorOrganizador(Guid organizadorId)
        {
            throw new NotImplementedException();
        }

        public Palestra ObterPorId(Guid id)
        {
            var palestraId = Guid.NewGuid();
            var endereco = new Endereco(Guid.NewGuid(), "", "", "", "", "", "", "", palestraId);            

            return Palestra.PalestraFactory.NovaPalestraCompleta(palestraId, "Dexter", "", "", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Supermix", Guid.NewGuid(), endereco, Guid.NewGuid());
        }

        public IEnumerable<Palestra> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
