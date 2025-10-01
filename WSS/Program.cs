using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WSS.API.Extensions;
using WSS.API.Infrastructure.Data;
using WSS.API.Mapping.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices();
builder.Services.AddIntegrationClients(builder.Configuration);
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ChangeRequestProfile).Assembly));
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CompletionReportProfile).Assembly));
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(WorkScheduleProfile).Assembly));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsql => npgsql.UseNetTopologySuite()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
