using Matrix.Common.Core;
using Matrix.Data;
using Matrix.Domain.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;

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

            var environment = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (environment.EnvironmentName == "Docker")
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationContext>();

                db.Database.Migrate();

                for (int i = 0; i < 10; i++)
                {
                    Dimension dim = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Dim {i}",
                        IsActive = true,
                        Order = i
                    };

                    Fact fact = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Fact {i}",
                        IsActive = true,
                        Order = i
                    };

                    db.Dimensions.Add(dim);
                    db.Facts.Add(fact);

                    db.SaveChanges();
                }

            }
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
