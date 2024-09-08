using System.Reflection;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Encoder API",
                    Version = "v1",
                    Description = "An API to access encoders from the EncoderHub library",
                    Contact = new OpenApiContact
                    {
                        Name = "Abel Nagy",
                        Email = "nagy.abel@edu.bme.hu",
                        Url = new Uri("https://github.com/nahu02"),
                    },
                }
            );
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // Swagger API documentation at /swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
