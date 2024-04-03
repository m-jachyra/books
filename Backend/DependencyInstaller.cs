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
            services.AddTransient(typeof(IServiceAsync<,,,>), typeof(ServiceAsync<,,,>));
            services.AddTransient<ReviewService>();
            services.AddTransient<BookService>();
            services.AddTransient<GenreService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<AuthService>();
            services.AddTransient<JwtManager>();
            services.AddTransient<IStorageService, StorageService>();

            return services;
        }
    }
}