using Data.EF.Context;
using Data.EF.Settings;
using Data.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data.EF.IoC
{
    public static class DataEFContainer
    {
        public static void AddBackendDataEF<TContext>(this IServiceCollection services, string connectionKey, IConfiguration configuration) where TContext : DbContextBase
        {
            services.AddDbContext<TContext>(optionsBuilder =>
            {
                var dbConfiguration = configuration.GetSection($"Databases:{connectionKey}").Get<DatabaseSettings>();
                switch (dbConfiguration.DatabaseType)
                {
                    case DatabaseType.SqlServer:
                        optionsBuilder.UseSqlServer(dbConfiguration.ConnectionString);
                        break;
                    case DatabaseType.SqlLite:
                        optionsBuilder.UseSqlite(dbConfiguration.ConnectionString);
                        break;
                    case DatabaseType.Postgre:
                        optionsBuilder.UseNpgsql(dbConfiguration.ConnectionString);
                        break;
                    default:
                        throw new NotSupportedException($"Name {dbConfiguration.DatabaseType}:is not supported");
                }
            }).AddScoped<DbContextBase, TContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
