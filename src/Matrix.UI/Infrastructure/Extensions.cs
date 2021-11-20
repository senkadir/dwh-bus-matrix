using Matrix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Matrix.UI.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), y =>
                {
                    y.MigrationsAssembly(typeof(Identifier).Namespace);
                }));
            }

            return services;
        }
    }
}
