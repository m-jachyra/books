using Backend.Auth;
using Backend.Repositories;
using Backend.Services;
using Backend.Services.Base;
using Backend.Storage;

namespace Backend
{
    public static class DependencyInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient(typeof(IServiceAsync<,>), typeof(ServiceAsync<,>));
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<BookService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IStorageService, StorageService>();

            return services;
        }
    }
}