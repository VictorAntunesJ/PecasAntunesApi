using PecasAntunes.Application.Interfaces;
using PecasAntunes.Application.Services;
using PecasAntunes.Infrastructure.Data;
using PecasAntunes.Infrastructure.Repositories;
using PecasAntunes.API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<IAutoPecaRepository, AutoPecaRepository>();
builder.Services.AddScoped<IAutoPecaService, AutoPecaService>();

// Swagger + XML Docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PecasAntunes API",
        Version = "v1",
        Description = "API para controle e gestão de autopeças",
        Contact = new OpenApiContact
        {
            Name = "Victor Sérgio",
            Url = new Uri("https://github.com/seu-github")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Versionamento de API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// -------------------- APP --------------------

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PecasAntunes API v1");
    });
}

// Middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("FrontEndPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
