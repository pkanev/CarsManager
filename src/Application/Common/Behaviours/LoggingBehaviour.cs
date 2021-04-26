using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CarsManager.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger logger;
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IUserService userService)
        {
            this.logger = logger;
            this.currentUserService = currentUserService;
            this.userService = userService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await userService.GetUserNameAsync(userId);
            }

            logger.LogInformation("CarsManager Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
