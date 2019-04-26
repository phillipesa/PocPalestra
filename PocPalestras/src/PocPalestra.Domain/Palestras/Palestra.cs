﻿using FluentValidation;
using PocPalestra.Domain.Core.Models;
using PocPalestra.Domain.Organizadores;
using System;
using System.Collections.Generic;

namespace PocPalestra.Domain.Palestras
{
    public class Palestra : Entity<Palestra>
    {
        #region props
        public string Nome { get; private set; }
        public string DescricaoAbrev { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public string NomeEmpresa { get; private set; }
        public bool Excluido { get; private set; }
        public Guid? CategoriaId { get; private set; }
        public Guid? EnderecoId { get; private set; }
        public Guid OrganizadorId { get; private set; }
        public ICollection<Tags> Tags { get; private set; }        
        #endregion

        #region EF propriedades de navegacao        
        public virtual Categoria Categoria { get; private set; }
        public virtual Endereco Endereco { get; private set; }
        public virtual Organizador Organizador { get; private set; }
        #endregion

        public void AtribuirEndereco(Endereco endereco)
        {
            if (!endereco.EhValido()) return;
            Endereco = endereco;
        }

        public void AtribuirCategoria(Categoria categoria)
        {
            if (!categoria.EhValido()) return;
            Categoria = categoria;
        }

        public void ExcluirEvento()
        {
            // TODO: Deve validar alguma regra?
            Excluido = true;
        }

        public void TornarPresencial()
        {
            // Alguma validacao de negocio?
            Online = false;
        }

        public Palestra(string nome, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
        }

        private Palestra() { }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações
        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();
            ValidationResult = Validate(this);

            // Validacoes adicionais das agregações
            ValidarEndereco();
        }

        private void ValidarNome()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome deve ser fornecido")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 carcteres");
        }
        private void ValidarValor()
        {
            if (!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000)
                    .WithMessage("O valor deve estar entre 1.00 e 50.000");

            if (Gratuito)
                RuleFor(c => c.Valor)
                    .Equal(0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve diferente de 0 para um evento gratuito");
        }
        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .LessThan(c => c.DataFim)
                .WithMessage("A data de início deve ser maior que a data do final do evento");

            RuleFor(c => c.DataInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("A data de início não deve ser menor que a data atual");
        }
        private void ValidarLocal()
        {
            if (Online)
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online)
                    .WithMessage("A palestra não deve possuir um endereço se for online");

            if (!Online)
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => c.Online == false)
                    .WithMessage("A palestra deve possuir um endereço");

        }
        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome do organizador precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do organizador precisa ter entre 2 e 150 caracteres");
        }
        private void ValidarEndereco()
        {
            if (Online) return;
            if (Endereco.EhValido()) return;

            foreach (var error in Endereco.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }
        #endregion

        #region Factory
        public static class PalestraFactory
        {
            public static Palestra NovaPalestraCompleta(Guid id, string nome, string descricaoAbrev, string descricao, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeEmpresa, Guid? organizadorId, Endereco endereco, Guid categoriaId)
            {
                var palestra = new Palestra()
                {
                    Id = id,
                    Nome = nome,
                    DescricaoAbrev = descricaoAbrev,
                    Descricao = descricao,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeEmpresa = nomeEmpresa,
                    Endereco = endereco,
                    CategoriaId = categoriaId
                };

                if (organizadorId.HasValue)
                    palestra.OrganizadorId = organizadorId.Value;

                if (online)
                    palestra.Endereco = null;

                return palestra;               
            }
        }

        #endregion
    }
}
