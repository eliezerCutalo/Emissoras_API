using Core_MVC.Data.Repository;
using Core_MVC.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Core_MVC.IoC.Mapping
{
    public static class Map
    {
        public static void AddConfigurations(IServiceCollection services)
        {

            //Mapear repositorios
            services.AddTransient<IEmissoraRepository, EmissoraRepository>();
            services.AddTransient<IAudienciaRepository, AudienciaRepository>();
        }
    }
}
