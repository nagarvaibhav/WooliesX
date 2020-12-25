using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WooliesX.Options;
using WooliesX.Provider;
using WooliesX.Services;

namespace WooliesX
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WooliesX-Api", Version = "v1" });
            });
            services.AddHttpClient();
            services.AddOptions();

            services.Configure<WooliexApiOptions>(Configuration.GetSection("WooliexApiOptions"));
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductDataProvider, ProductDataProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger(
              c =>
              {
                  c.RouteTemplate = "swagger/{documentName}/swagger.json";
              });

            app.UseSwaggerUI(c =>
            {

                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "WooliesX-Api-V1");
            });

            app.UseMvc();
        }
    }
}
