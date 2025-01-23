using System.Diagnostics;
using Application.Interfaces;

namespace Application.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUser user,
    IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUser _user = user ?? throw new ArgumentNullException(nameof(user));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly Stopwatch _timer = new();
    private const int ThresholdMilliseconds = 500;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > ThresholdMilliseconds)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
                userName = await _identityService.GetUserNameAsync(userId);


            _logger.LogWarning(message: "Solicitud de demostración de larga duración: {Name} ({ElapsedMilliseconds} milisegundos) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}