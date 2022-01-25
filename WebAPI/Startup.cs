using IOC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Configurations;
using Domain.ViewModels;
using ELibrary.Infrastructure.Persistence.Integrations.Paystack;
using ELibrary.Infrastructure.Persistence.Integrations.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WebAPI.Filters;
using WebAPI.ActionResults;

namespace WebAPI
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
            services.Configure<PaystackSettings>(Configuration.GetSection("Paystack"));
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddCors();

            services.AddDatabase(Configuration.GetConnectionString("LibraryDbContext"));
            services.Configure<BookReturnConfig>(Configuration.GetSection("BookReturnConfig"));
            services.AddFileExportService()
            .AddRepositories()
            .AddServices()
            .AddCustomIdentity()
            .AddBackgroundTasks()
            .AddLogging();
     
            services.AddHttpClient<IPaystackService, PaystackService>()
               .SetHandlerLifetime(TimeSpan.FromMinutes(5));

          

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtTokenSettings:TokenIssuer"],
                    ValidAudience = Configuration["JwtTokenSettings:TokenIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtTokenSettings:TokenKey"]))
                };
                options.RequireHttpsMetadata = false;
            });
            services.Configure<DataProtectionTokenProviderOptions>(o =>
              o.TokenLifespan = TimeSpan.FromHours(3));
            services.AddMvc(options =>
            {
                options.Filters.Add<RequestLoggingFilter>();
                options.Filters.Add<HttpGlobalExceptionFilter>();
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
                });
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailSender, MailSender>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HRSystem API",
                    Version = "v1",
                    Description = "HRSystem API",
                    Contact = new OpenApiContact
                    {
                        Name = "YuscomSoft Team",
                        Email = "yusfate81@yahoo.com",
                        Url = new Uri("https://codelearnershub.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "YuscomSoft Team",
                        Url = new Uri("https://codelearnershub.com"),
                    }
                });
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IDH API");
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
