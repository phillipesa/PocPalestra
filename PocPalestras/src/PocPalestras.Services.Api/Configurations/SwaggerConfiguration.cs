using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace PocPalestras.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "PocPalestra API",
                    Description = "API do site Palestras",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Phillipe Barcelos", Email = "phillipe.barcelos@gmail.com", Url = "http://palestra" },
                    License = new License { Name = "MIT", Url = "http://palestra/licensa" }
                });
                
                //s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            //services.ConfigureSwaggerGen(opt =>
            //{
            //    opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            //});
        }
    }
}
