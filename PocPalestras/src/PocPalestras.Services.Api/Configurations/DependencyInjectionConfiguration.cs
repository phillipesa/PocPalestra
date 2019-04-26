using Microsoft.Extensions.DependencyInjection;
using PocPalestra.Infra.CrossCutting.IoC;

namespace PocPalestras.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
