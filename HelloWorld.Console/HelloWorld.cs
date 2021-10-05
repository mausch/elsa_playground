using Elsa.Activities.Console;
using Elsa.Builders;

namespace HelloWorld
{
    public class HelloWorld: IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder.WriteLine("Hello world");
        }
    }
}