

using Microsoft.Extensions.DependencyInjection;
using ProjectR.Application.Abstractions;
using ProjectR.Domain.Abstractions;
using ProjectR.Infrastructure.Authentication;
using ProjectR.Infrastructure.Repositories;

namespace ProjectR.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEpicRepository, EpicRepository>();
            services.AddTransient<IThreadRepository, ThreadRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
