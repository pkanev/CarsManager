using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsManager.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private const int LIMIT = 500;

        private readonly Stopwatch timer;
        private readonly ILogger<TRequest> logger;
        private readonly ICurrentUserService currentUserService;
        private readonly IIdentityService identityService;

        public PerformanceBehaviour(
            ILogger<TRequest> logger,
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            timer = new Stopwatch();

            this.logger = logger;
            this.currentUserService = currentUserService;
            this.identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            timer.Start();

            var response = await next();

            timer.Stop();

            var elapsedMilliseconds = timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > LIMIT)
            {
                var requestName = typeof(TRequest).Name;
                var userId = currentUserService.UserId ?? string.Empty;
                var userName = string.Empty;

                if (!string.IsNullOrEmpty(userId))
                {
                    userName = await identityService.GetUserNameAsync(userId);
                }

                logger.LogWarning("CarsManager Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
