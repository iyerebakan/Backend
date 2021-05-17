using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Behaviours
{
    public class LoggingProcessor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingProcessor<TRequest, TResponse>> logger;

        public LoggingProcessor(ILogger<LoggingProcessor<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                this.logger.LogInformation($"Handling {typeof(TRequest).FullName}");
                var response = await next().ConfigureAwait(false);
                this.logger.LogInformation($"Handled {typeof(TResponse).FullName}");
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Handling failed {typeof(TResponse).FullName}");

                throw;
            }
        }
    }
}
