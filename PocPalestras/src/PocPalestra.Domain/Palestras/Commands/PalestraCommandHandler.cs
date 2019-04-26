using PocPalestra.Domain.CommandHandlers;
using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Interfaces;
using PocPalestra.Domain.Palestras.Events;
using PocPalestra.Domain.Palestras.Repository;
using System;

namespace PocPalestra.Domain.Palestras.Commands
{
    public class PalestraCommandHandler : CommandHandler,
        IHandler<RegistrarPalestraCommand>,
        IHandler<AtualizarPalestraCommand>,
        IHandler<ExcluirPalestraCommand>,
        IHandler<IncluirEnderecoPalestraCommand>,
        IHandler<AtualizarEnderecoPalestraCommand>
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IBus _bus;

        public PalestraCommandHandler(IPalestraRepository palestraRepository, IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _palestraRepository = palestraRepository;
            _bus = bus;
        }
        public void Handle(RegistrarPalestraCommand message)
        {
            var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero, message.Endereco.Complemento, message.Endereco.Bairro, message.Endereco.CEP, message.Endereco.Cidade, message.Endereco.Estado, message.Endereco.PalestraId.Value);

            var palestra = Palestra.PalestraFactory.NovaPalestraCompleta(message.Id, message.Nome, message.DescricaoAbrev,
               message.Descricao, message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
               message.Online, message.NomeEmpresa, message.OrganizadorId, endereco, message.CategoriaId);

            if (!PalestraValida(palestra)) return;

            // TODO:
            // validações de negocio!

            // Persistencia
            _palestraRepository.Adicionar(palestra);

            if (Commit())
            {
                // Notificar processo concluido
                Console.WriteLine("Evento Registrado com sucesso");
                _bus.RaiseEvent(new PalestraRegistradaEvent(palestra.Id, palestra.Nome, palestra.DataInicio, palestra.DataFim, palestra.Gratuito, palestra.Valor, palestra.Online, palestra.NomeEmpresa));
            }

        }

        public void Handle(ExcluirPalestraCommand message)
        {
            if (PalestraExistente(message.Id, message.MessageType)) return;

            _palestraRepository.Remover(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new PalestraExcluidaEvent(message.Id));
            }
        }

        public void Handle(AtualizarPalestraCommand message)
        {
            if (PalestraExistente(message.Id, message.MessageType)) return;

            var palestraAtual = _palestraRepository.ObterPorId(message.Id);

            var palestra = Palestra.PalestraFactory.NovaPalestraCompleta(message.Id, message.Nome, message.DescricaoAbrev,
                message.Descricao, message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
                message.Online, message.NomeEmpresa, message.OrganizadorId, palestraAtual.Endereco, message.CategoriaId);


            if (!PalestraValida(palestra)) return;

            _palestraRepository.Atualizar(palestra);

            if (Commit())
            {
                _bus.RaiseEvent(new PalestraAtualizadaEvent(message.Id, message.Nome, message.DataInicio, message.DataFim, message.Gratuito, message.Valor, message.Online, message.NomeEmpresa, message.DescricaoAbrev, message.Descricao));
            }
        }

        public void Handle(AtualizarEnderecoPalestraCommand message)
        {
            var endereco = new Endereco(message.Id, message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.CEP, message.Cidade, message.Estado, message.PalestraId.Value);
            if (!endereco.EhValido())
            {
                NotificarValidacoesErro(endereco.ValidationResult);
                return;
            }

            _palestraRepository.AtualizarEndereco(endereco);

            if (Commit())
            {
                //_bus.RaiseEvent(new EnderecoPalestraAtualizadaEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.CEP, endereco.Cidade, endereco.Estado, endereco.EventoId.Value));
            }

        }

        public void Handle(IncluirEnderecoPalestraCommand message)
        {
            var endereco = new Endereco(message.Id, message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.CEP, message.Cidade, message.Estado, message.PalestraId.Value);
            if (!endereco.EhValido())
            {
                NotificarValidacoesErro(endereco.ValidationResult);
                return;
            }

            var palestra = _palestraRepository.ObterPorId(message.PalestraId.Value);
            palestra.TornarPresencial();

            _palestraRepository.Atualizar(palestra);
            _palestraRepository.AdicionarEndereco(endereco);

            if (Commit())
            {
                //_bus.PublicarEvento(new EnderecoEventoAdicionadoEvent(endereco.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.CEP, endereco.Cidade, endereco.Estado, endereco.EventoId.Value));
            }            
        }

        private bool PalestraValida(Palestra palestra)
        {
            if (palestra.EhValido()) return true;

            NotificarValidacoesErro(palestra.ValidationResult);
            return false;
        }

        private bool PalestraExistente(Guid id, string messageType)
        {
            var palestra = _palestraRepository.ObterPorId(id);

            if (palestra != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Palestra js cadastrada"));
            return false;
        }

        
    }
}

