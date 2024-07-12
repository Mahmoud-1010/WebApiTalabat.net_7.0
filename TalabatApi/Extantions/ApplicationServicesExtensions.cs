using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using BusinessLogicLayer.Services;
using Talabat.BLL.Repositories;
using Talabat.BLL.Services;
using TalabatApi.Helpers;

namespace TalabatApi.Extantions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped<IOrderService,OrderService>();
            return services;
        }

    }
}
