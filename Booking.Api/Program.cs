using Booking.Application;
using Booking.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterApplication();

builder.Services.RegisterInfrastructure(
    builder.Configuration);

//var connectionString = builder.Configuration
//    .GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<BookingDbContext>(options =>
//    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();