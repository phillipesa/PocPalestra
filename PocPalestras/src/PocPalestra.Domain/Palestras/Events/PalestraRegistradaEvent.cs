using System;

namespace PocPalestra.Domain.Palestras.Events
{
    public class PalestraRegistradaEvent : BasePalestraEvent
    {
        public PalestraRegistradaEvent(Guid id, string nome, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeEmpresa)
        {
            Id = id;
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;            
            AggregateId = id;
        }
    }
}
