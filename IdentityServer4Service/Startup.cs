using IdentityServer4Service.IdentityServerConfig.Extension;
using IdentityServer4Service.IdentityServerConfig.IdentityNeeds;
using IdentityServer4Service.MiddlewareHeaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Constants.connStr = configuration.GetConnectionString("SqlSeverConnStrings");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Middleware Identity Server 4
            services.AddIdentityServer()
                .AddInMemoryApiResources(ApiResources.GetApis())
                .AddInMemoryIdentityResources(ApiResources.GetIdentity())
                .AddInMemoryApiScopes(ApiResources.GetApiScopes())
                .AddInMemoryClients(Clients.GetClient())
                .AddDeveloperSigningCredential()
                .AddTestUsers(UsersTemp.Get());
            //.AddIdentityServer4User();

            services.AddCors();
            services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Register Custom Header Middleware
            //app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
            //    .AddDefaultSecurePolicy()
            //    .AddCustomHeader("X-Online-Based-Education", "Online Based Education"));

            app.UseRouting();
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Register Identity Server 4
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
