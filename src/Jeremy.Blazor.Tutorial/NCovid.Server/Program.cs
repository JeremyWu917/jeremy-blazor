using Microsoft.EntityFrameworkCore;
using NCovid.Server.Data;
using NCovid.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Êý¾Ý¿â
builder.Services.AddDbContext<NCovidDbCntext>(options => options.UseSqlite("Data Source=NCovidDb.db3"));

builder.Services.AddScoped<IDerpartmentRepository, DerpartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDailyHealthRepository, DailyHealthRepository>();

builder.Services.AddEndpointsApiExplorer();

// ¿çÓò
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", _builder =>
    {
        _builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("Open");

app.UseAuthorization();

app.MapControllers();

app.Run();
