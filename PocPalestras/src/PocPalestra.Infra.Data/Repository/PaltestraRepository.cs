using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PocPalestra.Domain.Palestras;
using PocPalestra.Domain.Palestras.Repository;
using PocPalestra.Infra.Data.Context;

namespace PocPalestra.Infra.Data.Repository
{
    public class PaltestraRepository : Repository<Palestra>, IPalestraRepository
    {
        public PaltestraRepository(PalestraContext context) : base(context)
        {

        }
        public void AdicionarEndereco(Endereco endereco)
        {
            Db.Enderecos.Add(endereco);
        }

        public override IEnumerable<Palestra> ObterTodos()
        {
            var sql = "SELECT * FROM PALESTRAS E " +
                      "WHERE E.EXCLUIDO = 0 " +
                      "ORDER BY E.DATAFIM DESC ";

            return Db.Database.GetDbConnection().Query<Palestra>(sql);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Db.Enderecos.Update(endereco);
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            var sql = @"SELECT * FROM Categorias";
            return Db.Database.GetDbConnection().Query<Categoria>(sql);
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            var sql = @"SELECT * FROM Enderecos E " +
                     "WHERE E.Id = @uid";

            var endereco = Db.Database.GetDbConnection().Query<Endereco>(sql, new { uid = id });

            return endereco.SingleOrDefault();
        }

        public Palestra ObterMinhaPalestraPorId(Guid id, Guid organizadorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Palestra> ObterPalestraPorOrganizador(Guid organizadorId)
        {
            var sql = @"SELECT * FROM PALESTRAS E " +
                      "WHERE E.EXCLUIDO = 0 " +
                      "AND E.ORGANIZADORID = @oid " +
                      "ORDER BY E.DATAFIM DESC";

            return Db.Database.GetDbConnection().Query<Palestra>(sql, new { oid = organizadorId });
        }      

        public override Palestra ObterPorId(Guid id)
        {
            var sql = @"SELECT * FROM PALESTRAS E " +
                      "LEFT JOIN Enderecos EN " +
                      "ON E.Id = EN.EventoId " +
                      "WHERE E.Id = @uid";

            var evento = Db.Database.GetDbConnection().Query<Palestra, Endereco, Palestra>(sql,
                (e, en) =>
                {
                    if (en != null)
                        e.AtribuirEndereco(en);

                    return e;
                }, new { uid = id });

            return evento.FirstOrDefault();
        }

        public override void Remover(Guid id)
        {
            var palestra = ObterPorId(id);
            palestra.ExcluirEvento();
            Atualizar(palestra);
        }
    }
}
