using Zamazimah.Core.Utils.Mailing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Zamazimah.Api.Extensions;
using Zamazimah.Api.Filters;
using Zamazimah.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Zamazimah.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string appRoot { get; set; }
        public IWebHostEnvironment Environement { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            // Set up root path
            this.appRoot = env.ContentRootPath;
            this.Environement = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddDbContext(this.Environement, this.appRoot);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // add validation filter
            services.AddScoped<ModelValidationAttribute>();
            services.AddRepository();
            services.AddTransientServices();
            // ===== Add Jwt Authentication ========
            services.AddJWTAuthentification(Configuration);

            services.AddAuthorization();

            services.AddResponseCompression();

            services.AddMvc();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    //  builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials();



                    builder.WithOrigins(Configuration["AllowedOrigins"].Split(","))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();


                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ZamazimahContext appContext)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources")),
                RequestPath = "/Resources"
            });

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            // }
            app.ConfigureExceptionHandler(Configuration);

            app.UseResponseCompression();
            //    app.ConfigureExceptionHandler(this.AppSettings);
            // ===== Use Authentication ======
            app.UseAuthentication();
            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zamazimah V1");
            });
            app.UseRouting();



        

            app.UseCors("cors");
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // run migrations & database initialisation
            appContext.Database.Migrate();
        }

    }
}
