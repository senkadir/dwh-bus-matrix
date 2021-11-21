using Matrix.Common.Core;
using Matrix.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Matrix.UI.Infrastructure
{
    internal static class Extensions
    {
        internal static IServiceCollection AddContext(this IServiceCollection services)
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

        internal static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var db = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            db.Database.Migrate();
        }

        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IServiceBase, ServiceBase>();

            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(Core.Identifier))
                    .AddClasses(x => x.AssignableTo<IServiceBase>())
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }
    }
}
