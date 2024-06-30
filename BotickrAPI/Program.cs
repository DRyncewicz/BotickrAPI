using BotickrAPI.Application.Extensions;
using BotickrAPI.Application.Helpers;
using BotickrAPI.Domain.SettingsOptions.Swagger;
using BotickrAPI.Extensions;
using BotickrAPI.GlobalHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
    });
builder.Services.Configure<SwaggerOptions>(builder.Configuration.GetSection(SwaggerOptions.AppsettingsKey));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddPersistenceDI(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
}

if (app.Environment.EnvironmentName != "Testing")
{
    app.MigrateDatabase();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program();