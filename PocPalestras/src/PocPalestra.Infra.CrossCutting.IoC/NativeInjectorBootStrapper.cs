using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PocPalestra.Domain.Core.Bus;
using PocPalestra.Domain.Core.Events;
using PocPalestra.Domain.Core.Notifications;
using PocPalestra.Domain.Interfaces;
using PocPalestra.Domain.Organizadores.Commands;
using PocPalestra.Domain.Organizadores.Events;
using PocPalestra.Domain.Organizadores.Repository;
using PocPalestra.Domain.Palestras.Commands;
using PocPalestra.Domain.Palestras.Events;
using PocPalestra.Domain.Palestras.Repository;
using PocPalestra.Infra.CrossCutting.AspNetFilters;
using PocPalestra.Infra.CrossCutting.Identity.Models;
using PocPalestra.Infra.CrossCutting.Identity.Services;
using PocPalestra.Infra.Data.Context;
using PocPalestra.Infra.Data.Repository;
using PocPalestra.Infra.Data.Uow;
using PocPalestras.Infra.CrossCutting.Bus;

namespace PocPalestra.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            //services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IBus, InMemoryBus>();

            // Domain - Commands
            services.AddScoped<IHandler<RegistrarPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IHandler<AtualizarPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IHandler<ExcluirPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IHandler<AtualizarEnderecoPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IHandler<IncluirEnderecoPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IHandler<RegistrarOrganizadorCommand>, OrganizadorCommandHandler>();

            // Domain - Eventos
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IHandler<PalestraRegistradaEvent>, PalestraEventHandler>();
            services.AddScoped<IHandler<PalestraAtualizadaEvent>, PalestraEventHandler>();
            services.AddScoped<IHandler<PalestraExcluidaEvent>, PalestraEventHandler>();
            services.AddScoped<IHandler<EnderecoPalestraAtualizadoEvent>, PalestraEventHandler>();
            services.AddScoped<IHandler<EnderecoPalestraAdicionadoEvent>, PalestraEventHandler>();
            services.AddScoped<IHandler<OrganizadorRegistradoEvent>, OrganizadorEventHandler>();

            // Infra - Data
            services.AddScoped<IPalestraRepository, PaltestraRepository>();
            services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<PalestraContext>();            

            // Infra - Data EventSourcing
            //services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            //services.AddScoped<IEventStore, SqlEventStore>();
            //services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();
        }
    }
}
