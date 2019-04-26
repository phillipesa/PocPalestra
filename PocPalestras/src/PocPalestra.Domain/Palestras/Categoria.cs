using PocPalestra.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace PocPalestra.Domain.Palestras
{
    public class Categoria : Entity<Categoria>
    {
        #region Props
        public string Nome { get; private set; }
        #endregion

        #region EF Propriedade de Navegação
        public virtual ICollection<Palestra> Palestra { get; set; }
        #endregion

        public Categoria(Guid id)
        {
            Id = id;
        }

        // Construtor para o EF
        protected Categoria() { }

        public override bool EhValido()
        {
            return true;
        }
    }
}