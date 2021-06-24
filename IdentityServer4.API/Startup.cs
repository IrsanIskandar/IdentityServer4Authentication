using IdentityServer4.API.IdentityServerFilterOperation;
using IdentityServer4.API.WebAppsHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Constants.connStr = configuration.GetConnectionString("MysqlConnString");
            // Base Url
            Constants.IdentityServerBaseUrl = configuration.GetSection("AuthServer").GetValue<string>("IdentityBaseUrl");
            Constants.WebApiAppBaseUrl = configuration.GetSection("AuthServer").GetValue<string>("WebApiAppBaseUlr");
            // Web Api Secret
            Constants.WebApiName = configuration.GetSection("WebApiSecret").GetValue<string>("WebApiName");
            Constants.WebApiClientId = configuration.GetSection("WebApiSecret").GetValue<string>("WebApiClientId");
            Constants.WebApiClientSecret = configuration.GetSection("WebApiSecret").GetValue<string>("WebApiClientSecret");
            // Mobile Api Secret
            Constants.MobileApiName = configuration.GetSection("MobileApiSecret").GetValue<string>("MobileApiName");
            Constants.MobileApiClientId = configuration.GetSection("MobileApiSecret").GetValue<string>("MobileApiClientId");
            Constants.MobileApiClientSecret = configuration.GetSection("MobileApiSecret").GetValue<string>("MobileApiClientSecret");
            // Identity Server 4 Secret
            Constants.ApiSecret = configuration.GetSection("IdentityServer4Config").GetValue<string>("ApiSecret");
            Constants.WebApiScope = configuration.GetSection("IdentityServer4Config").GetValue<string>("WebApiScope");
            Constants.MobileApiScope = configuration.GetSection("IdentityServer4Config").GetValue<string>("MobileApiScope");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore()
                    .AddApiExplorer()
                    .AddAuthorization()
                    .AddNewtonsoftJson();
            services.AddWebEncoders();
            services.AddDistributedMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Constants.IdentityServerBaseUrl);
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            // Register Privileged Role
            services.AddAuthorization(roleOption =>
            {
                roleOption.AddPolicy("DbAdmin", policy => policy.RequireRole("SuperAdmin"));
                roleOption.AddPolicy("SysAdmin", policy => policy.RequireRole("Admin"));
                roleOption.AddPolicy("MgmtStaff", policy => policy.RequireRole("Staff"));
                roleOption.AddPolicy("MgmtVisitor", policy => policy.RequireRole("Visitor"));
                roleOption.AddPolicy("Everyone", policy => policy.RequireRole("SuperAdmin", "Admin", "Staff", "Visitor"));
            });

            // Register Identity Server 4 Auth Access Token
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", authOptions =>
                {
                    authOptions.Authority = Constants.IdentityServerBaseUrl;
                    authOptions.RequireHttpsMetadata = false;
                    // Register Api
                    authOptions.ApiName = Constants.WebApiName;
                    authOptions.ApiSecret = Constants.ApiSecret;

                    authOptions.SaveToken = true;
                    authOptions.RequireHttpsMetadata = false;
                    //authOptions.ClaimsIssuer = Constants.Authority;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer4.API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(Constants.IdentityServerBaseUrl + "/connect/token", UriKind.Absolute),
                            AuthorizationUrl = new Uri(Constants.IdentityServerBaseUrl + "/connect/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                { Constants.WebApiScope, "Identity Server 4 Web API Service" },
                                { Constants.MobileApiScope, "Identity Server 4 Mobile API Service" }
                            }
                        }
                    }
                    //Reference = new OpenApiReference
                    //{
                    //    Id = "Bearer",
                    //    Type = ReferenceType.SecurityScheme
                    //}
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer4.API v1");

                    c.OAuthAppName("Identity Server Auth Web API V1");
                    c.OAuthClientId(Constants.WebApiClientId);
                    c.OAuthClientSecret(Constants.WebApiClientSecret);
                    c.OAuthScopeSeparator(" ");
                    c.EnableDeepLinking();
                    c.OAuthUsePkce();
                    
                    c.OAuth2RedirectUrl(Constants.IdentityServerBaseUrl + "/oauth/token/");
                });
            }

            app.UseCors(options =>
            {
                options.WithOrigins(Constants.IdentityServerBaseUrl);
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
