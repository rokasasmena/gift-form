namespace GiftFormAPI.Services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddTransient<IGiftService, GiftService>();
            services.AddTransient<IChildService, ChildService>();

            return services;
        }
    }
}