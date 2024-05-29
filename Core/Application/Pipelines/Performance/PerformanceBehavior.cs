using MediatR;
using System.Diagnostics;

namespace Core.Application.Pipelines.Performance
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IIntervalRequest
    {

        private readonly Stopwatch _stopwatch;

        public PerformanceBehavior(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requstName = request.GetType().Name;

            TResponse response;

            try
            {
                _stopwatch.Start();
                return await next();
            }
            finally
            {
                if (_stopwatch.Elapsed.TotalSeconds > request.Interval)
                {
                    string message = $"Performance -> {requstName} {_stopwatch.Elapsed.TotalSeconds} s";

                    Debug.WriteLine(message);
                }
                _stopwatch.Restart();
            }
            return response;
        }
    }
}
