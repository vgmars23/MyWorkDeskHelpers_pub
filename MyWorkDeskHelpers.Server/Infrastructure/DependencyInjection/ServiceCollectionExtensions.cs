using MyWorkDeskHelpers.Application.Interfaces;
using MyWorkDeskHelpers.Server.Infrastructure.Configurations;

namespace MyWorkDeskHelpers.Server.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));

            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
