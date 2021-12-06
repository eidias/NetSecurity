using Chinook.Core.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Consider using the DbContext factory in cases where lifetime is not scoped.
            var sqliteConnection = new SqliteConnection(@$"datasource='{AppContext.BaseDirectory}\Databases\chinook.sqlite'");
            services.AddDbContext<ChinookContext>(options =>
            {
                options.UseSqlite(sqliteConnection);
#if DEBUG
                //[DEMO] Combine this with custom compilation symbols. Always consider potential build issues for non-compilation paths.
                options.EnableSensitiveDataLogging();
#endif
            });

            //[LINK] https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks
            services.AddHealthChecks()
                .AddDbContextCheck<ChinookContext>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddRazorPages();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chinook.Web", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Always ensure the request pipeline is unsing the proper sequence.
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseBrowserLink();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chinook.Web v1"));
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
