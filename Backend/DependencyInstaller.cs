using Backend.Auth;
using Backend.Repositories;
using Backend.Services.Base;

namespace Backend
{
    public static class DependencyInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient(typeof(IServiceAsync<,>), typeof(ServiceAsync<,>));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtManager, JwtManager>();

            return services;
        }
    }
}