using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public class RegistrarPalestraCommand : BasePalestraCommand
    {
        public IncluirEnderecoPalestraCommand Endereco { get; private set; }
        public RegistrarPalestraCommand(
            string nome,
            string descricaoAbrev,
            string descricao,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeEmpresa,
            Guid organizadorId,
            Guid categoriaId,
            IncluirEnderecoPalestraCommand endereco
            )
        {
            Nome = nome;
            DescricaoAbrev = descricaoAbrev;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
            OrganizadorId = organizadorId;
            CategoriaId = categoriaId;
            Endereco = endereco;
        }
    }
}
