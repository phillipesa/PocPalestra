using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Interfaces;
using PocPalestra.Domain.Palestras.Commands;
using PocPalestra.Domain.Palestras.Repository;
using PocPalestras.Services.Api.ViewModels;

namespace PocPalestras.Services.Api.Controllers
{
    public class PalestrasController : BaseController
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public PalestrasController(IDomainNotificationHandler<DomainNotification> notifications,
                                 IUser user,
                                 IPalestraRepository palestraRepository,
                                 IMapper mapper,
                                 IBus bus) : base(notifications, user, bus)
        {
            _palestraRepository = palestraRepository;
            _mapper = mapper;
            _bus = bus;
        }

        [HttpGet]
        [Route("palestras")]
        [AllowAnonymous]
        public IEnumerable<PalestraViewModel> Get()
        {
            return _mapper.Map<IEnumerable<PalestraViewModel>>(_palestraRepository.ObterTodos());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestras/categorias")]
        public IEnumerable<CategoriaViewModel> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(_palestraRepository.ObterCategorias());
        }

        [HttpGet]
        [Authorize(Policy = "PodeLer")]
        [Route("palestras/meus-eventos")]
        public IEnumerable<PalestraViewModel> ObterMeusEventos()
        {
            return _mapper.Map<IEnumerable<PalestraViewModel>>(_palestraRepository.ObterPalestraPorOrganizador(OrganizadorId));
        }

        [HttpGet]
        [Authorize(Policy = "PodeLer")]
        [Route("palestras/minhas-plestras/{id:guid}")]
        public IActionResult ObterMeuEventoPorId(Guid id)
        {
            var evento = _mapper.Map<PalestraViewModel>(_palestraRepository.ObterMinhaPalestraPorId(id, OrganizadorId));
            return evento == null ? StatusCode(404) : Response(evento);
        }

        [HttpPost]
        [Route("palestras")]
        [Authorize(Policy = "PodeGravar")]        
        public IActionResult Post([FromBody]PalestraViewModel palestraViewModel)
        {
            if (!ModelStateValid())
            {
                return Response();
            }

            var palestraCommand = _mapper.Map<RegistrarPalestraCommand>(palestraViewModel);

            _bus.SendCommand(palestraCommand);
            return Response(palestraCommand);
        }

        [HttpPut]
        [Route("palestras")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Put([FromBody]PalestraViewModel palestraViewModel)
        {
            if (!ModelStateValid())
            {
                return Response();
            }

            var palestraCommand = _mapper.Map<AtualizarPalestraCommand>(palestraViewModel);

            _bus.SendCommand(palestraCommand);
            return Response(palestraCommand);
        }

        [HttpDelete]
        [Route("palestras/{id:guid}")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Delete(Guid id)
        {
            var palestraViewModel = new PalestraViewModel { Id = id };
            var palestraCommand = _mapper.Map<ExcluirPalestraCommand>(palestraViewModel);

            _bus.SendCommand(palestraCommand);
            return Response(palestraCommand);
        }

        [HttpPost]
        [Route("endereco")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Post([FromBody]EnderecoViewModel enderecoViewModel)
        {
            if (!ModelStateValid())
            {
                return Response();
            }

            var palestraCommand = _mapper.Map<IncluirEnderecoPalestraCommand>(enderecoViewModel);

            _bus.SendCommand(palestraCommand);
            return Response(palestraCommand);
        }

        [HttpPut]
        [Route("endereco")]
        [Authorize(Policy = "PodeGravar")]
        public IActionResult Put([FromBody]EnderecoViewModel enderecoViewModel)
        {
            if (!ModelStateValid())
            {
                return Response();
            }

            var palestraCommand = _mapper.Map<AtualizarEnderecoPalestraCommand>(enderecoViewModel);

            _bus.SendCommand(palestraCommand);
            return Response(palestraCommand);
        }

        private bool ModelStateValid()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }

    }
}