using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Models;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Http
{
    public record PassValuesToSuspendedActivities(
        IServiceProvider _services,
        ILogger<PassValuesToSuspendedActivities> _logger
    ): IHostedService
    {
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(11), cancellationToken);

                using var scope = _services.CreateScope();
                var launchpad = scope.ServiceProvider.GetRequiredService<IWorkflowLaunchpad>();

                var q = new WorkflowsQuery<SampleBlockingActivity>(new BM());
                await launchpad.CollectAndDispatchWorkflowsAsync(q, input: new WorkflowInput("input for blocked activity"), cancellationToken: cancellationToken);
                _logger.LogInformation("Passing input to blocked activities");
            }, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}