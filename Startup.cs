using Demos.API.Application.Contracts;
using Demos.API.Application.Models;
using Demos.API.Application.Services;
using Demos.API.Filters;
using Demos.API.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Demos.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(provider => provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddScoped<INasa, Nasa>();
            services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

            //Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen();
            services.AddSwaggerGen(content =>
            {
                content.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "Demos API V1",
                        Description = "Demos API V1",
                        Version = "v1"
                    });
                content.EnableAnnotations();
                content.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                /*
                content.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                content.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() },
                }); 
                */

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                content.IncludeXmlComments(xmlPath);

            });

            services.AddMediatR(typeof(Startup));

            // Add API Versioning to the service container to your project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            // Add HttpClientFactory  without base address
            //services.AddHttpClient();

            // Add HttpClientFactory with base addres
            services.AddHttpClient("base", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.nasa.gov/insight_weather/?api_key=wHFHLf2JGLZwYpki5vreuF9OLzB5KByRRhgN8ELI&feedtype=json&ver=1.0");
            });

            // Add custom httpclient by dependency injection
            services.AddHttpClient<CustomHttpClient>("custom", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.nasa.gov/insight_weather/?api_key=wHFHLf2JGLZwYpki5vreuF9OLzB5KByRRhgN8ELI&feedtype=json&ver=1.0");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomMiddleware();
            app.UseCustomMiddleware2();
            //Set Swagger API documentation
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DocumentTitle = "My API V1";
                config.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                config.RoutePrefix = string.Empty;
            });

            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
