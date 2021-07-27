using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using ServicioAPISeguridad.Transversal.IoC;
using System;
using System.Text;

namespace ServicioAPISeguridad.Services.WebAPI
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IoC.AddRegistration(services);

            //añadir nlog
            services.AddLogging(l =>
            {
                l.SetMinimumLevel(LogLevel.Information);
                l.AddNLog("NLog.config");
                
            });

            //add cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                                        
                                                          
                                  });
            });

            //wt
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Jwt:IssuerToken"],
                    ValidAudience = Configuration["Jwt:audienceToken"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                        
                };
            });

            //añadir swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "ASP.NET Core Web API Core"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //se añade la configuracion para swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","MyAPI");
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

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
