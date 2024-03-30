using Backend.Auth;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Backend.Services.Base;

namespace Backend
{
    public static class DependencyInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient(typeof(IServiceAsync<,>), typeof(ServiceAsync<,>));
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtManager, JwtManager>();

            return services;
        }
    }
}