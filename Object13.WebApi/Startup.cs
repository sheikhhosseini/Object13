using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.Core.Services.Implementations;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Extention;
using Object13.DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Object13.Core.Email;
using Object13.Core.Security;

namespace Object13.WebApi
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _HostEnvironment = hostEnvironment;
            Configuration = configuration;  
        }
        public IHostEnvironment _HostEnvironment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region appsettings.json
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json").Build());
            #endregion

            #region ConectionString
            services.Object13ApplicationDbContext(Configuration);
            #endregion

            #region DbContext
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region ForEmail-RazorView
            services.AddControllersWithViews();
            services.AddRazorPages();
            #endregion

            #region AppServices
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<IMailSender, SendEmail>();
            services.AddScoped<IViewRenderService, RenderViewToString>();
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44345/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Object13JwtBearer"))
                };
            });
            #endregion

            #region Cors
            services.AddCors(option =>
            {
                option.AddPolicy("Object13CorsPolicy" , builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build();
                });
            });
            #endregion

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }




            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();




            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseCors("Object13CorsPolicy");
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
