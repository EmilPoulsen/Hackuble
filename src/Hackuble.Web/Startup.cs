using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using CompileBlazorInBlazor;

namespace Hackuble.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CompileService>();
            services.AddSingleton<CommandService>();
            services.AddSingleton<LoaderService>();
            services.AddSingleton<Context>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
