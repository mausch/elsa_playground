using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;

namespace HelloWorld.Http
{
    public class HeartbeatActivity: IActivity
    {
        public async ValueTask<bool> CanExecuteAsync(ActivityExecutionContext context)
        {
            await Task.CompletedTask;
            return true;
        }

        public async ValueTask<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Heartbeat from activity at {DateTimeOffset.Now}");
            return new DoneResult();
        }

        public async ValueTask<IActivityExecutionResult> ResumeAsync(ActivityExecutionContext context)
        {
            await Task.CompletedTask;
            return new DoneResult();
        }

        public string Type => this.GetType().Name;
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool PersistWorkflow { get; set; }
        public bool LoadWorkflowContext { get; set; }
        public bool SaveWorkflowContext { get; set; }
        public IDictionary<string, object?> Data { get; set; }
    }
}