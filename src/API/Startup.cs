using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Data;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Categories;
using Data.Repositories.Transactions;
using Services.Categories;
using Services.Transactions;
using Npgsql;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace src
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

            //if using kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                // options.AllowSynchronousIO = true;
            });

            //if using IIS
            services.Configure<IISServerOptions>(options =>
            {
                // options.AllowSynchronousIO = true;
            });

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Personal Finance Management API",
                    Version = "v1",
                    Description = " ",
                });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(CreateConnectionString());
                options.UseNpgsql(npgsqlOptionsAction: x => x.MigrationsAssembly("API"));
            });

            //repos
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();

            //services
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "API v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Import MerchantCodes
            AppDbExtension.SeedData();
        }

        private string CreateConnectionString()
        {
            string _username = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? this.Configuration["Database:Username"];
            string _password = Environment.GetEnvironmentVariable("DATABASE_PASSOWRD") ?? this.Configuration["Database:Password"];
            string _host = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? this.Configuration["Database:Host"];
            string _port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? this.Configuration["Database:Port"];
            string _database = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? this.Configuration["Database:Name"];

            var builder = new NpgsqlConnectionStringBuilder()
            {
                Username = _username,
                Password = _password,
                Host = _host,
                Port = int.Parse(_port),
                Database = _database,
                Pooling = true,
                // Timeout = ?
            };

            return builder.ConnectionString;
        }
    }
}