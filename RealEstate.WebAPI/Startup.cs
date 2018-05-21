namespace RealEstate.WebAPI
{
    using System;
    using System.Reflection;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using NSwag.AspNetCore;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // Add framework services.
            services.AddMvc(o =>
            {
                o.OutputFormatters.RemoveType<StringOutputFormatter>();
                o.Filters.Add(typeof(RequireHttpsAttribute));
                o.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute));
                // o.Filters.Add(typeof(ProactContextTransactionFilter));
                // o.Filters.Add(typeof(ApiExceptionFilter));
                // o.Filters.Add(typeof(ExceptionLoggingFilter));
            });

            services.AddAuthentication();

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<RealEstateContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Proact.Default"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                var swaggerUiSettings = new SwaggerUiSettings
                {
                    DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase,
                    IsAspNetCore = true
                };

                app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, swaggerUiSettings);

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("allowAngular");

            app.UseMvc();

            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
