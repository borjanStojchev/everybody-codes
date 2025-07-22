using Microsoft.OpenApi.Models;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Services;

namespace SecurityCameras.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICsvDataService, CsvDataDataService>();
        builder.Services.AddSingleton<ICameraSearchService, CameraSearchService>();

        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SecurityCameras.API",
                Version = "v1"
            });
        });

        var app = builder.Build();
        
        app.UseCors();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SecurityCameras.API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}