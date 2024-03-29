using Microsoft.EntityFrameworkCore;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.Infrastructure;
using TravelEasy.EV.Infrastructure.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<ElectricVehiclesContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ElectricVehiclesContext")));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IElectricVehicleService, ElectricVehicleService>();
builder.Services.AddTransient<IBookingService, BookingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
