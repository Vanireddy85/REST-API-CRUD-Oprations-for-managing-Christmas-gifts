using ChristmasWishList.BusinessLayer.Services;
using ChristmasWishList.BusinessLayer.Services.Interfaces;
using ChristmasWishList.BusinessLayer.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ChristmasWishList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<SaveGiftRequestValidator>();

            builder.Services.AddScoped<IGiftService, GiftService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ChristmasWishList",
                    Version = "V1",
                    Description = "A simple API to manage Christmas gifts",
                    Contact = new OpenApiContact
                    {
                        Name = "Vani Reddy",
                        Email = "vanichinthireddy@gmail.com",

                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);


            })
            .AddFluentValidationRulesToSwagger(options =>
            {
                options.SetNotNullableIfMinLengthGreaterThenZero = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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