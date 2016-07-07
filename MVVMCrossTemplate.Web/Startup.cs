using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MVVMCrossTemplate.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            var baseaddress = Configuration["AzureB2C:BaseAddress"];
            var policy = Configuration["AzureB2C:PolicyName"];
            var audienceId = Configuration["AzureB2C:AudienceId"];
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Audience = audienceId,
                ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                   metadataAddress: $"https://login.microsoftonline.com/{baseaddress}/v2.0/.well-known/openid-configuration?p={policy}",
                   configRetriever: new OpenIdConnectConfigurationRetriever(),
                   docRetriever: new HttpDocumentRetriever() { RequireHttps = false })
            });


            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
