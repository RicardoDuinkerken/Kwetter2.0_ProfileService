
using GenericDal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.Api.Controllers;
using ProfileService.Core.Services.Interfaces;
using ProfileService.Dal.Context;
using ProfileService.Dal.Models;

namespace ProfileService.Api;

    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //database connection
            string server = Configuration["Server"] ?? "localhost";
            string port = Configuration["Port"] ?? "3306";
            string username = Configuration["Username"] ?? "root";
            string password = Configuration["Password"] ?? "Geheim_101";
            string database = Configuration["Database"] ?? "Profile";
            string connectionString = $"server={server};Port={port};user={username};password={password};database={database}";

            services.AddDbContext<ProfileServiceContext>(builder =>
                builder.UseMySQL(connectionString));

            //repositories
            services
                .AddTransient<IAsyncRepository<Profile, long>,
                    BaseAsyncRepository<Profile, long, ProfileServiceContext>>();

            //services  
            services.AddTransient<IProfileService, Core.Services.ProfileService>();

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProfileServiceContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.Database.EnsureCreated();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProfileController>();

                endpoints.MapGet("/",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                    });
            });
        }
    }