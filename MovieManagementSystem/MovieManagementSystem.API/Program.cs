using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Controllers;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Implementation;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // DI Award interface and its implementation
            builder.Services.AddScoped<IAwardRepo, AwardRepo>();
            builder.Services.AddScoped<IMovieRepo, MovieRepo>();
            builder.Services.AddScoped<ICountryRepo, CountryRepo>();
            builder.Services.AddScoped<IDirectorRepo, DirectorRepo>();
            builder.Services.AddScoped<ILanguageRepo, LanguageRepo>();

            builder.Services.AddScoped<IGeneric<User>, GenericRepo<User>>();
            builder.Services.AddScoped<IGeneric<Genre>, GenericRepo<Genre>>();
            builder.Services.AddScoped<IGeneric<Booking>, GenericRepo<Booking>>();
            builder.Services.AddScoped<IGeneric<Review>, GenericRepo<Review>>();
            builder.Services.AddScoped<IGeneric<Actor>, GenericRepo<Actor>>();


            // Databse creating database configuration options 
            builder.Services.AddDbContext<MovieDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("connection"))); // connection to the databse
            var app = builder.Build();


            //CORS policy to talk between angular and backend
            app.UseCors(policy => policy.
            AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            );


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) // because of this graphics things will run
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}