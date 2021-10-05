using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Http
{
    public record SampleBlockingActivity(
        ILogger<SampleBlockingActivity> _logger
    ) : IActivity
    {
        public async ValueTask<bool> CanExecuteAsync(ActivityExecutionContext context)
        {
            await Task.CompletedTask;
            return true;
        }

        public async ValueTask<IActivityExecutionResult> ExecuteAsync(ActivityExecutionContext context)
        {
            _logger.LogInformation($"ExecuteAsync with input {context.Input}");
            await Task.CompletedTask;
            if (context.WorkflowExecutionContext.IsFirstPass == false)
            {
                return new SuspendResult();
            }

            return await ResumeAsync(context);
        }

        public async ValueTask<IActivityExecutionResult> ResumeAsync(ActivityExecutionContext context)
        {
            _logger.LogInformation($"ResumeAsync with input '{context.Input}', '{context.WorkflowExecutionContext.Input}'");
            await Task.CompletedTask;
            context.Output = context.Input;
            return new DoneResult();
        }

        public string Type => GetType().Name;
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public bool PersistWorkflow { get; set; }
        public bool LoadWorkflowContext { get; set; }
        public bool SaveWorkflowContext { get; set; }
        public IDictionary<string, object?> Data { get; set; }
    }
    
    public class BM: IBookmark {}

    public class SampleBlockingBMProvider : ABookmarkProvider<BM, SampleBlockingActivity>
    {
        readonly ILogger<SampleBlockingBMProvider> _logger;

        public SampleBlockingBMProvider(ILogger<SampleBlockingBMProvider> logger)
        {
            _logger = logger;
        }

        protected override async ValueTask<IReadOnlyList<BookmarkResult<BM>>> GetBookmarksAsyncT(BookmarkProviderContext<SampleBlockingActivity> context, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetBookmarksAsyncT for activity ID {context.ActivityExecutionContext.ActivityId} workflow {context.ActivityExecutionContext.WorkflowInstance.ContextId}");
            await Task.CompletedTask;
            return new[]
            {
                new BookmarkResult<BM>(new BM())
            };
        }
    }
}