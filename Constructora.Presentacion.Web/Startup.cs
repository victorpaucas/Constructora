using Constructora.Presentacion.Web.Cross;
using Constructora.Presentacion.Web.Data;
using Constructora.Presentacion.Web.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Constructora.Presentacion.Web
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvcCore()
                .AddNewtonsoftJson();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://favidev.com/",
                    ValidAudience = "https://favidev.com/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RmF2aWRldkZhdmlkZXY"))
                };
            });
            services.AddDbContext<MainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConstructoraConnection")));
            services.AddControllersWithViews();
            services.AddTransient<IUserTypeRepository, UserTypeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddSingleton(typeof(ILogger), services.BuildServiceProvider().GetService<ILogger<ExceptionCross>>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development.");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation("Not Development.");
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors();
            app.UseMiddleware<ExceptionCross>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller=User}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "usertype",
                    pattern: "{controller=UserType}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "file",
                    pattern: "{controller=File}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "upload",
                    pattern: "{controller=Upload}/{action=Index}/{id?}");
            });
        }
    }
}
