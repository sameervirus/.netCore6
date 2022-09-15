using System.Text.Json.Serialization;
using ArconsSystem.Api.Common;
using ArconsSystem.Api.Common.Middlewares;
using ArconsSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddDbContext<DataContext>(
       options => options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")));
    builder.Services
        .AddAuth();
}

var app = builder.Build();

{
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}