namespace RealEstate.WebAPI
{
    using System;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using NSwag.AspNetCore;
    using Serilog;

    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // Add framework services.
            services.AddMvc(o =>
            {
                o.OutputFormatters.RemoveType<StringOutputFormatter>();
                o.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute));
                o.Filters.Add(typeof(RealEstateContextTransactionFilter));
                o.Filters.Add(typeof(ApiExceptionFilter));
                o.Filters.Add(typeof(ExceptionLoggingFilter));
            }).AddFeatureFolders();

            services.AddAuthentication();

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<RealEstateContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("RealEstate"));
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime appLifetime)
        {
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    NJsonSchema.PropertyNameHandling.CamelCase;
            });

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();

            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
