using System.Net;
using Elsa;
using Elsa.Activities.Http;
using Elsa.Builders;

namespace HelloWorld.Http
{
    public class HelloWorldWorkflow: IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .HttpEndpoint("/hello-world")
                .When(OutcomeNames.Done)
                .WriteHttpResponse(HttpStatusCode.OK, "<h1>Hello world</h1>", "text/html");
        }
    }
}