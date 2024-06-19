namespace TalabatApi.Extantions
{
    public static class SwaggerServicesExtentions
    {
        public static IServiceCollection AddSwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static WebApplication UseSwaggerMiddleWare(this WebApplication webApplication)
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
            return webApplication;
        }
    }
}
