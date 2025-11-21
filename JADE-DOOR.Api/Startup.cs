using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using JADE_DOOR.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace JADE_DOOR.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // 🔥 调试：检查配置有没有读到
            Console.WriteLine("DEBUG => Auth0 Authority = " + Configuration["Auth0:Authority"]);
            Console.WriteLine("DEBUG => Auth0 Audience = " + Configuration["Auth0:Audience"]);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Read Auth0 values from appsettings.json
            string authority = Configuration["Auth0:Authority"]
                ?? throw new ArgumentNullException("Auth0:Authority");

            string audience = Configuration["Auth0:Audience"]
                ?? throw new ArgumentNullException("Auth0:Audience");

            // Controllers
            services.AddControllers();

            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
            });

            // Authorization Policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("delete:catalog", policy =>
                    policy.RequireAuthenticatedUser()
                          .RequireClaim("scope", "delete:catalog"));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Database
            services.AddDbContext<StoreContext>(options =>
                options.UseSqlite("Data Source=../Registrar.sqlite",
                b => b.MigrationsAssembly("JADE-DOOR.Data")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JADE-DOOR.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors();

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
