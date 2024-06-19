using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using TalabatApi.Helpers;

namespace TalabatApi.Extantions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }

    }
}
