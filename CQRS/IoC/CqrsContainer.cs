using CQRS.Commands;
using CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.IoC
{
    public static class CqrsContainer
    {
        public static void AddBackendCqrs(this IServiceCollection services)
        {
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<ICommandBus, CommandBus>();
        }
    }
}
