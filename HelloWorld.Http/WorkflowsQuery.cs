using Elsa.Services;
using Elsa.Services.Models;

namespace HelloWorld.Http
{
    public record WorkflowsQuery<TActivity> : WorkflowsQuery
        where TActivity: IActivity
    {
        public WorkflowsQuery(IBookmark? Bookmark, string? CorrelationId = null, string? WorkflowInstanceId = null, string? ContextId = null, string? TenantId = null) : 
            base(typeof(TActivity).Name, Bookmark, CorrelationId, WorkflowInstanceId, ContextId, TenantId)
        {
        }
    }
}