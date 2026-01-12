using PecasAntunes.Application.Interfaces;
using PecasAntunes.Application.Services;
using PecasAntunes.Infrastructure.Data;
using PecasAntunes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PecasAntunes.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("FrontEndPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
