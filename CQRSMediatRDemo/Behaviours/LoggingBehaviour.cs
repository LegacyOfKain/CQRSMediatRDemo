using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public ILogger<LoggingBehaviour<TRequest, TResponse>> logger { get; }

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // This pipeline based logging behaviour tracks only "CQRS" requests and responses

            // Pre logic
            var requestName = request.GetType();
            logger.LogDebug($"{requestName} is starting  ");
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();

            // Post logic
            logger.LogInformation("{Request} has finished in {Time}ms.", requestName, timer.ElapsedMilliseconds);
            return response;
        }
    }
}
