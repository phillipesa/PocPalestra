using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public class AtualizarPalestraCommand : BasePalestraCommand
    {
        public AtualizarPalestraCommand(Guid id, string nome, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeEmpresa, string descricaoAbrev, string descricao)
        {
            Id = id;
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
            DescricaoAbrev = descricaoAbrev;
            Descricao = descricao;
        }
    }
}
