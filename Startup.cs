using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloWorldFromDB
{
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

            services.AddTransient<HelloWorldRepositoryContext>(
                serviceProvider => new HelloWorldRepositoryContextFactory().CreateDbContext(new string[]
                    {
                        Configuration.GetConnectionString("HWDatabase")
                    }
                )
            );
            services.AddSwaggerGen();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game API");
            });
        }
    }

    public class HelloWorldRepositoryContextFactory : IDesignTimeDbContextFactory<HelloWorldRepositoryContext>
    {
        public HelloWorldRepositoryContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server = (localdb)\mssqllocaldb; Database=LocalHWdb; Trusted_Connection=True; ";
            if (args.Length != 0)
            {
                connectionString = args[0];
            }
            var contextOptions = new DbContextOptionsBuilder<HelloWorldRepositoryContext>()
                .UseSqlServer(connectionString, options => { options.CommandTimeout(180); })
                .Options;
            return new HelloWorldRepositoryContext(contextOptions);
        }
    }
}
