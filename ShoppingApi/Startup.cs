using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingApi.Domain;
using ShoppingApi.Hubs;
using ShoppingApi.Profiles;
using ShoppingApi.Services;
using System.Text.Json.Serialization;

namespace ShoppingApi
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
                options.AddDefaultPolicy(pol =>
                {
                    pol.WithOrigins("http://localhost:4200");
                    pol.AllowAnyHeader();
                    pol.AllowAnyMethod();
                    pol.AllowCredentials(); // You have to do this for WebSockets.
                });
            });

            // We can inject in the ShoppingDataContext 
            services.AddDbContext<ShoppingDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("shopping"));
            });

            services.AddScoped<ILookupProducts, EfProducts>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Api", Version = "v1" });
            });

            var systemTime = new SystemTime(); // THE instance everything will use.
            services.AddSingleton<ISystemTime>(systemTime); // Add it as singleton like this.

            var productProfile = new ProductProfiles(systemTime); // Manually pass it in.
            MapperConfiguration config = new MapperConfiguration(options =>
            {
                options.AddProfile(productProfile); // Use that instance here.
                options.AddProfile<CurbsideOrdersProfile>();
                // add more later...
            });

            services.AddSingleton<IMapper>(config.CreateMapper());
            services.AddSingleton<MapperConfiguration>(config);

            services.Configure<ConfigurationForPricing>(
                Configuration.GetSection(ConfigurationForPricing.SectionName)
                );


            services.AddScoped<IProcessCurbsideOrders, EfOrderProcessor>();

            services.AddSingleton<CurbsideOrderChannel>();
            services.AddHostedService<BackgroundOrderProcessor>();

            services.AddSignalR().AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                // options.PayloadSerializerOptions.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingApi v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ShoppingHub>("/shoppinghub");
                endpoints.MapControllers();
            });
        }
    }
}
