using System.Text.Json.Serialization;
using aspnetUserAuthTemplate.Modules;
using aspnetUserAuthTemplate.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace aspnetUserAuthTemplate;

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
            var jwtSigningKey = Configuration.GetValue<string>("Authorization:JWTSigningKey");
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services
                .AddSingleton<IPasswordHasher, Argon2IdHasher>()
                .AddDbContext<DatabaseContext>()
                .AddSingleton<DbAccess>()
                .AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                    ? new JWTAuthorization()
                    : new JWTAuthorization(jwtSigningKey))
                .AddSingleton<IConfiguration>(Configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Watchify API endpoints", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(options =>
                {
                    options
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
                options.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }