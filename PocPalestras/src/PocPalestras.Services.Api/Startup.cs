using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PocPalestra.Infra.CrossCutting.AspNetFilters;
using PocPalestra.Infra.CrossCutting.Identity.Data;
using PocPalestras.Infra.CrossCutting.Bus;
using PocPalestras.Services.Api.Configurations;
using System;
using System.Text;

namespace PocPalestras.Services.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IConfigurationRoot Configuration { get; }

        private const string SecretKey = "pocpalestra@datacubotoken";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Contexto do EF para o Identity            
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            // Options para configurações customizadas
            services.AddOptions();

            // MVC com restrição de XML e adição de filtro de ações.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
            });          

            // Versionamento do WebApi
            services.AddApiVersioning("api/v{version}");

            // Configurações do Swagger
            services.AddSwaggerConfig();

            // AutoMapper
            services.AddAutoMapper();

            // MediatR
            //services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                ILoggerFactory loggerFactory,
                                IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            #region Logging

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //var elmahSts = new ElmahIoSettings
            //{
            //    OnMessage = message =>
            //    {
            //        message.Version = "v1.0";
            //        message.Application = "Eventos.IO";
            //        message.User = accessor.HttpContext.User.Identity.Name;
            //    },
            //};

            //loggerFactory.AddElmahIo("e1ce5cbd905b42538c649f6e1d66351e", new Guid("adee8feb-4afb-4d2c-859d-30f729d47793"));
            //app.UseElmahIo("e1ce5cbd905b42538c649f6e1d66351e", new Guid("adee8feb-4afb-4d2c-859d-30f729d47793"), elmahSts);

            #endregion

            #region Configurações MVC

                app.UseCors(c =>
                {
                    c.AllowAnyHeader();
                    c.AllowAnyMethod();
                    c.AllowAnyOrigin();
                });

                app.UseStaticFiles();
                app.UseAuthentication();
                app.UseMvc();

            #endregion

            #region Swagger
                if (env.IsProduction())
                {
                    // Se não tiver um token válido no browser não funciona.
                    // Descomente para ativar a segurança.
                    // app.UseSwaggerAuthorized();
                }            
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Palestra API V1.0");
                });
            #endregion

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }
    }
}
