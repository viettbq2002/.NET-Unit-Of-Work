using Microsoft.AspNetCore.HttpLogging;
using UnitOfWork.Infrastructure.ServiceExtensions;
using UnitOfWork.Services;
using UnitOfWork.Services.Implements;
using UnitOfWork.Services.Interfaces;
using WebAPI.Filter;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(_ => _.Filters.Add<ErrorHandlingFilterAttribute>());
builder.Services.AddHttpLogging(o =>

{
    o.LoggingFields = HttpLoggingFields.RequestPath |
                                        HttpLoggingFields.RequestMethod |
                                        HttpLoggingFields.RequestBody |
                                        HttpLoggingFields.ResponseStatusCode;

    o.CombineLogs = true;

    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration).AddService();

var app = builder.Build();
app.UseHttpLogging();

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
