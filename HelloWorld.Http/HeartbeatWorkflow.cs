using System;
using System.Threading.Tasks;
using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace HelloWorld.Http
{
    public record HeartbeatWorkflow: IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .Timer(Duration.FromSeconds(5))
                .Then<HeartbeatActivity>()
                .Then(async () =>
                {
                    await Task.CompletedTask;
                    Console.WriteLine($"Heartbeat from inline activity at {DateTimeOffset.Now}");
                })
                .WriteLine(context => $"Heartbeat from writeline at {DateTimeOffset.Now}")
                .Then<SampleBlockingActivity>()
                .WriteLine(context => $"After blocking activity at {DateTimeOffset.Now}. Output is '{context.Output}'")
                ;
        }
    }
}