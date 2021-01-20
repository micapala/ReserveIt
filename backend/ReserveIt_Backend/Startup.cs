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
using System.Diagnostics;

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
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IEmailService, EmailService>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IConcertRepository, ConcertRepository>();
            services.AddSingleton<IBandRepository, BandRepository>();
            services.AddSingleton<IPaymentRepository, PaymentRepository>();
            services.AddSingleton<IReservationRepository, ReservationRepository>();
        }


        internal static IConfiguration Configuration { get; private set; }

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
            app.UsePathBase(new PathString("/api"));

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

            AddRandomBands(context);
            AddRandomConcerts(context);

            var testUser1 = new Models.User
            {
                Username = "login",
                Password = "password",
                Name = "Imie",
                Surname = "Nazwisko",
                Email = "asd@asd.pl"
            };

            context.Users.Add(testUser1);
            context.SaveChanges();
        }

        private static void AddRandomBands(ApiContext context)
        {
            String[] colors = { "Red", "Purple", "Indigo", "Orange", "Maroon" };
            String[] animals = { "Mastodon", "Gecko", "Elephant", "Possum", "Chimpanzee" };

            foreach (String color in colors)
            {
                foreach (String animal in animals)
                {
                    var band = new Models.Band
                    {
                        Name = color + " " + animal,
                    };
                    context.Bands.Add(band);
                }
            }

            context.SaveChanges();
        }

        private static void AddRandomConcerts(ApiContext context)
        {
            String[] plants = { "Bee Balm Flower", "Bluebonnet", "Thistle", "Agapanthus", 
                                "Banyan", "Kentia Palm", "Rosemary", "Baobab", "Nemesia", "Narcissus",
                                "Clarkia", "Huckleberry", "Aspen", "Carrot", "Tulip", "Silene", "Primrose",
                                "Bindweed", "Pansy", "Fellenwort", "Snowdrop", "Lucky Bamboo", "Cactus", "Lily",
                                "Dumb Cane", "Cotton plant", "Daisy", "Shasta Daisy", "Veronica", "Fennel",
                                "Gillyflower", "Rose", "Aspen", "Cotton plant", "Indian paintbrush", "Carrot",
                                "Bleeding Heart", "Water Lily", "Vervain", "Cucumber", "Cornflower" };
            String[] types = { "Festival", "Happening", "Concert", "Event" };

            Random random = new Random();
            for (var day = new DateTime(2021, 1, 22); day.Date <= new DateTime(2021, 1, 31); day = day.AddDays(1))
            {
                int num = random.Next(2, 7);
                for (int i = 0; i < num; i++)
                {
                    int i1 = random.Next(0, plants.Length);
                    int i2 = random.Next(0, types.Length);
                    int bandID = random.Next(1, context.Bands.Count());
                    float price = 0.99f + (float)random.Next(10, 50);

                    var concert = new Models.Concert
                    {
                        Name = plants[i1] + " " + types[i2],
                        Band = context.Bands.Find(bandID),
                        Date = day,
                        TicketPrice = price
                    };
                    Debug.Assert(concert.Band != null);
                    context.Concerts.Add(concert);
                }
            }

            context.SaveChanges();
        }
    }
}
