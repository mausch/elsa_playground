using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloWorld.Http
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<PassValuesToSuspendedActivities>();
            
            services.AddElsa(options => options
                //.UseEntityFrameworkPersistence(ef => ef.UseSqlite(), autoRunMigrations: true)
                .AddHttpActivities(http => http.BaseUrl = new Uri("http://localhost:5000"))
                .AddConsoleActivities()
                .AddQuartzTemporalActivities()
                .AddJavaScriptActivities()
                
                .AddActivity<HeartbeatActivity>()
                .AddActivity<SampleBlockingActivity>()

                .AddWorkflow<HelloWorldWorkflow>()
                .AddWorkflow<HeartbeatWorkflow>()
                .Services.AddBookmarkProvider<SampleBlockingBMProvider>()
            );

            services.AddElsaApiEndpoints();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpActivities();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}