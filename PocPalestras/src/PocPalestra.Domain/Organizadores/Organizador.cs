using PocPalestra.Domain.Core.Models;
using PocPalestra.Domain.Palestras;
using System;
using System.Collections.Generic;

namespace PocPalestra.Domain.Organizadores
{
    public class Organizador : Entity<Organizador>
    {
        #region Props
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        #endregion

        #region EF Navegação
        public virtual ICollection<Palestra> Palestras { get; set; }
        #endregion

        public Organizador(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
        private Organizador() { }
        public override bool EhValido()
        {
            return true;
        }
    }
}