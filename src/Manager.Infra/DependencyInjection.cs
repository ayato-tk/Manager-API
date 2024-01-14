using Manager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ManagerContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly(typeof(ManagerContext).Assembly.FullName));
                }
            );
            return services;
        }
    }
}