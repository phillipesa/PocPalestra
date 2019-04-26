﻿using PocPalestra.Domain.Core.Commands;
using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public abstract class BasePalestraCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string DescricaoAbrev { get; protected set; }
        public string Descricao { get; protected set; }
        public DateTime DataInicio { get; protected set; }
        public DateTime DataFim { get; protected set; }
        public bool Gratuito { get; protected set; }
        public decimal Valor { get; protected set; }
        public bool Online { get; protected set; }
        public string NomeEmpresa { get; protected set; }
        public Guid OrganizadorId { get; protected set; }
        public Guid CategoriaId { get; protected set; }
    }
}
