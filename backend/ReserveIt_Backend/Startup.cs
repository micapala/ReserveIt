using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ReserveIt_Backend.Services.Interfaces;
using ReserveIt_Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using ReserveIt_Backend.Repositories;
using ReserveIt_Backend.Services;
using ReserveIt_Backend.Helpers;
using Microsoft.Extensions.Logging;

namespace ReserveIt_Backend
{
    public class Startup
    {
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IBandService, BandService>();
            services.AddTransient<IConcertService, ConcertService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IConcertRepository, ConcertRepository>();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            ConfigureTransientServices(services);
            ConfigureRepositories(services);
            services.AddDbContext<ApiContext>();

            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            services.AddCors();

            services.AddMvc();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReserveIt_Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "ReserveIt_Backend v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(x => x.MapControllers());

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiContext>();
                AddTestData(context);
            }

        }

        private static void AddTestData(ApiContext context)
        {

            var admin = new Models.User
            {
                Username = "admin",
                Password = "admin",
                Name = "Administrator",
                Surname = "Serwisu",
                Email = "kontakt@ReserveIt.pl",
                Role = "Admin"
            };

            context.Users.Add(admin);

            var testUser1 = new Models.User
            {
                Username = "login",
                Password = "password",
                Name = "Imie",
                Surname = "Nazwisko",
                Email = "asd@asd.pl"
            };

            context.Users.Add(testUser1);

            var testBand1 = new Models.Band
            {
                Name = "Ryder and The Straight Bustas"
            };

            context.Bands.Add(testBand1);

            context.SaveChanges();

            Console.WriteLine(context.Bands.Single(name => name.Name == "Ryder and The Straight Bustas").Name);

            var concert1 = new Models.Concert
            {
                Band = context.Bands.Single(b => b.Name == "Ryder and The Straight Bustas"), //Doesn't work
                Date = new DateTime(2020, 12, 13),
                TicketPrice = 10
            };

            context.Concerts.Add(concert1);

            context.SaveChanges();
            Console.WriteLine(context.Concerts.Single(name => name.TicketPrice == 10).Band.Name);
        }
    }
}
