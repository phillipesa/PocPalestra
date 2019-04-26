using PocPalestra.Domain.Core.Commands;
using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public class IncluirEnderecoPalestraCommand : Command
    {
        #region Props
        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? PalestraId { get; private set; }
        #endregion

        public IncluirEnderecoPalestraCommand(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? palestraId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            PalestraId = palestraId;
        }    
    }
}
