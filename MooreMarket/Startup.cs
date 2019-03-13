using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MooreMarket.Data;
using MooreMarket.Helpers;
using MooreMarket.Services;


namespace MooreMarket
{
  public class Startup
    {
        private readonly ILogger _logger;
        
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
              .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<MooreMarketContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            if (appSettings == null || (String.IsNullOrEmpty(appSettings.Secret))) {
              _logger.LogError("No secret key provided in appsettings");
              throw new ArgumentNullException("appSettings.secret is null or empty");
            }
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
              x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
              // x.Events = new JwtBearerEvents
              // {
              //   OnTokenValidated = context =>
              //   {
              //     var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
              //     var userId = int.Parse(context.Principal.Identity.Name);
              //     var user = userService.GetById(userId);
              //     if(user == null)
              //     {
              //       context.Fail("Unauthorized");                  
              //     }
              //     return Task.CompletedTask;
              //   }
              // };
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
              };
            });
            
            
            
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //enables cors globally, not needed for development while we are using the devServer proxy

            // app.UseCors(x => x
            //   .AllowAnyOrigin()
            //   .AllowAnyMethod()
            //   .AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
